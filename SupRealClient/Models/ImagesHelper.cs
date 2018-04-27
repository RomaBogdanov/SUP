using SupContract;
using SupRealClient.TabsSingleton;
using System;
using System.Data;
using System.IO;

namespace SupRealClient.Models
{
    public static class ImagesHelper
    {
        private const string Images = "Images";

        public static void Init()
        {
            if (!Directory.Exists(Images))
            {
                Directory.CreateDirectory(Images);
            }
        }

        public static string AddImageSource(int id, string path, ImageType imageType)
        {
            DataRow row = null;
            foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<int>("f_visitor_id") == id &&
                    r.Field<int>("f_image_type") == (int)imageType)
                {
                    row = r;
                    break;
                }
            }
            bool find = row != null;
            row = row ?? ImagesWrapper.CurrentTable().Table.NewRow();
            var alias = Guid.NewGuid();
            row["f_image_alias"] = alias;
            if (!find)
            {
                row["f_visitor_id"] = id;
                row["f_image_type"] = imageType;
                ImagesWrapper.CurrentTable().Table.Rows.Add(row);
            }
            byte[] data = File.ReadAllBytes(path);

            string image = "";
            if (ImagesWrapper.CurrentTable().Connector.SetImage(alias, data))
            {
                image = Directory.GetCurrentDirectory() + "\\" + Images + "\\" + alias;
                File.WriteAllBytes(image, data);
            }

            return image;
        }

        public static void RemoveImageSource(int id, ImageType imageType)
        {
            DataRow row = null;
            foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<int>("f_visitor_id") == id &&
                    r.Field<int>("f_image_type") == (int)imageType)
                {
                    row = r;
                    break;
                }
            }
            if (row != null)
            {
                row.Delete();
            }
        }

        public static string GetImage(int id, ImageType imageType)
        {
            DataRow row = null;
            foreach (DataRow r in ImagesWrapper.CurrentTable().Table.Rows)
            {
                if (r.Field<int>("f_visitor_id") == id &&
                    r.Field<int>("f_image_type") == (int)imageType)
                {
                    row = r;
                    break;
                }
            }

            string source = "";
            if (row != null)
            {
                string path = Directory.GetCurrentDirectory() + "\\" + Images + "\\" + row["f_image_alias"];
                if (!File.Exists(path))
                {
                    byte[] data =
                        ImagesWrapper.CurrentTable().Connector.GetImage((Guid)row["f_image_alias"]);
                    if (data != null)
                    {
                        File.WriteAllBytes(path, data);
                    }
                    else
                    {
                        path = "";
                    }
                }
                source = path;
            }
            return source;
        }
    }
}
