using SupContract;
using SupRealClient.TabsSingleton;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SupRealClient.Models
{
    public static class DocumentsHelper
    {
        /// <summary>
        /// Кэшируем изображения для документа
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public static List<Guid> CacheImages(int documentId)
        {
            var imageCache = new List<Guid>(
                from imgdoc in ImageDocumentWrapper.CurrentTable().Table.AsEnumerable()
                join image in ImagesWrapper.CurrentTable().Table.AsEnumerable()
                on imgdoc.Field<int>("f_image_id") equals image.Field<int>("f_image_id")
                where imgdoc.Field<string>("f_deleted") == "N" &&
                image.Field<string>("f_deleted") == "N" &&
                imgdoc.Field<int>("f_doc_id") == documentId &&
                image.Field<int>("f_image_type") == (int)ImageType.Document
                select image.Field<Guid>("f_image_alias"));

            foreach (var img in imageCache)
            {
                ImagesHelper.CacheImage(img);
            }

            return imageCache;
        }
    }
}
