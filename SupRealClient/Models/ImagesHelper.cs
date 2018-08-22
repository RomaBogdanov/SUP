using SupContract;
using SupRealClient.TabsSingleton;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace SupRealClient.Models
{
    /// <summary>
    /// Класс для работы с изображениями
    /// </summary>
    public static class ImagesHelper
    {
        private const string Images = "Images";

        /// <summary>
        /// Инициализация - создание папки Images в проекте
        /// </summary>
        public static void Init()
        {
            if (!Directory.Exists(Images))
            {
                Directory.CreateDirectory(Images);
            }
        }

        /// <summary>
        /// Загружаем картинку и сохраняем в папке Images
        /// по Guid
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Guid LoadImage(string path)
        {
            var alias = Guid.NewGuid();
            byte[] data = File.ReadAllBytes(path);

            string image = GetImagePath(alias);
            File.WriteAllBytes(image, data);

            return alias;
        }

	    /// <summary>
	    /// Сохраняем в папке Images, получаем GUID
	    /// </summary>
	    /// <returns></returns>
	    public static Guid GetGuidFromByteArray(byte[] imageArray)
	    {
		    var alias = Guid.NewGuid();
		    var imagePath = GetImagePath(alias);
		    File.WriteAllBytes(imagePath, imageArray);

		    return alias;
	    }

	    /// <summary>
        /// Получаем Guid картинки из базы по id посетителя
        /// и кэшируем ее в Images если необходимо
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        public static Guid GetImage(int id, ImageType imageType)
        {
            DataRow row = null;
            foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
            {
                if (Find(r, id, imageType, Guid.Empty))
                {
                    row = r;
                    break;
                }
            }

            Guid alias = Guid.Empty;
            if (row != null)
            {
                alias = (Guid)row["f_image_alias"];
                string path = GetImagePath(alias);
                if (!File.Exists(path))
                {
                    byte[] data =
                        ImagesWrapper.CurrentTable().Connector.GetImage(alias);
                    if (data != null)
                    {
                        File.WriteAllBytes(path, data);
                    }
                    else
                    {
                        alias = Guid.Empty;
                    }
                }
            }
            return alias;
        }

        /// <summary>
        /// Сохраняем картинку из базы на диск
        /// </summary>
        public static void CacheImage(Guid alias)
        {
            string path = GetImagePath(alias);
            if (!File.Exists(path))
            {
                byte[] data =
                    ImagesWrapper.CurrentTable().Connector.GetImage(alias);
                if (data != null)
                {
                    File.WriteAllBytes(path, data);
                }
            }
        }

        /// <summary>
        /// Путь к картинке по Guid
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static string GetImagePath(Guid alias)
        {
            return alias.Equals(Guid.Empty) ? "" :
                Directory.GetCurrentDirectory() +
                "\\" + Images + "\\" + alias;
        }

        /// <summary>
        /// Добавляем картинку в БД
        /// </summary>
        /// <param name="id"></param>
        /// <param name="alias"></param>
        /// <param name="imageType"></param>
        public static void AddImages(int id, List<KeyValuePair<Guid, ImageType>> images)
        {
            Dictionary<Guid, byte[]> imagesToSave = new Dictionary<Guid, byte[]>();
            foreach (var image in images)
            {
                if (image.Key.Equals(Guid.Empty))
                {
                    RemoveImage(id, image.Value);
                    continue;
                }

                DataRow row = null;
                foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
                {
                    if (Find(r, id, image.Value, image.Key))
                    {
                        row = r;
                        break;
                    }
                }

                if (row == null)
                {
                    row = ImagesWrapper.CurrentTable().Table.NewRow();
                    row["f_image_alias"] = image.Key;
                    row["f_visitor_id"] = id;
                    row["f_image_type"] = image.Value;
                    row["f_deleted"] = "N";
                    ImagesWrapper.CurrentTable().Table.Rows.Add(row);
                    imagesToSave.Add(image.Key,
                        File.ReadAllBytes(GetImagePath(image.Key)));
                }
                else if (image.Value == ImageType.Document)
                {
                    imagesToSave[image.Key] = File.ReadAllBytes(GetImagePath(image.Key));
                }
                else if (!image.Key.Equals(row["f_image_alias"]))
                {
                    row["f_image_alias"] = image.Key;
                    row["f_deleted"] = "N";
                    imagesToSave.Add(image.Key,
                        File.ReadAllBytes(GetImagePath(image.Key)));
                }
            }
            if (imagesToSave.Any())
            {
                ImagesWrapper.CurrentTable().Connector.SetImages(imagesToSave);
            }
        }

		/// <summary>
		/// Добавляем картинку в БД
		/// </summary>
		/// <param name="id"></param>
		/// <param name="alias"></param>
		/// <param name="imageType"></param>
		public static void AddImage_ByImageID(int imageID, int visitorID, Guid imageData, ImageType imageType)
		{
			Dictionary<Guid, byte[]> imagesToSave = new Dictionary<Guid, byte[]>();

				if (imageData.Equals(Guid.Empty))
				{
				RemoveImage_ByImageID(imageID, imageType);
					return;
				}

				DataRow row = null;
			if (imageID >= 0)
			{
				foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
				{
					if (Find_ByImageID(r, imageID, imageType, imageData))
					{
						row = r;
						break;
					}
				}
			}

				if (row == null)
				{
					row = ImagesWrapper.CurrentTable().Table.NewRow();
					row["f_image_alias"] = imageData;
					row["f_visitor_id"] = visitorID;
					row["f_image_type"] = imageType;
					row["f_deleted"] = "N";
					ImagesWrapper.CurrentTable().Table.Rows.Add(row);
					imagesToSave.Add(imageData,
						File.ReadAllBytes(GetImagePath(imageData)));
				}
				else if (imageType == ImageType.Document)
				{
					imagesToSave[imageData] = File.ReadAllBytes(GetImagePath(imageData));
				}
				else if (!imageData.Equals(row["f_image_alias"]))
				{
					row["f_image_alias"] = imageData;
					row["f_deleted"] = "N";
					imagesToSave.Add(imageData,
						File.ReadAllBytes(GetImagePath(imageData)));
				}
			if (imagesToSave.Any())
			{
				ImagesWrapper.CurrentTable().Connector.SetImages(imagesToSave);
			}
		}


		private static void RemoveImage(int id, ImageType imageType)
        {
            DataRow row = null;
            foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
            {
                if (Find(r, id, imageType, Guid.Empty))
                {
                    row = r;
                    break;
                }
            }
            if (row != null)
            {
                row["f_deleted"] = "Y";
                //row.Delete(); // TODO
            }
        }

		private static void RemoveImage_ByImageID(int imageID, ImageType imageType)
		{
			DataRow row = null;
			foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
			{
				if (Find_ByImageID(r, imageID, imageType, Guid.Empty))
				{
					row = r;
					break;
				}
			}
			if (row != null)
			{
				row["f_deleted"] = "Y";
				//row.Delete(); // TODO
			}
		}

		private static bool Find_ByImageID(DataRow row, int imageID, ImageType imageType, Guid alias)
		{
			return imageType == ImageType.Document ?
				row.Field<int>("f_image_id") == imageID &&
				row.Field<Guid>("f_image_alias") == alias &&
				row.Field<int>("f_image_type") == (int)imageType &&
				row.Field<string>("f_deleted") == "N" :
				row.Field<int>("f_image_id") == imageID &&
				row.Field<int>("f_image_type") == (int)imageType &&
				row.Field<string>("f_deleted") == "N";
		}

		private static bool Find(DataRow row, int id, ImageType imageType, Guid alias)
        {
            return imageType == ImageType.Document ?
                row.Field<int>("f_visitor_id") == id &&
                row.Field<Guid>("f_image_alias") == alias &&
                row.Field<int>("f_image_type") == (int)imageType &&
                row.Field<string>("f_deleted") == "N" :
                row.Field<int>("f_visitor_id") == id &&
                row.Field<int>("f_image_type") == (int)imageType &&
                row.Field<string>("f_deleted") == "N";
        }
    }
}
