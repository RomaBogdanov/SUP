-- �������� �� VisitorsImages �� T-SQL
create database VisitorsImages;
go

use VisitorsImages;
go

-- �������� vis_image
CREATE TABLE vis_image
    (f_image_id                    int NOT NULL,
    f_image_alias                  UNIQUEIDENTIFIER NOT NULL,
    f_visitor_id                   int,
    f_image_type                   int,
    f_data                         VARBINARY(MAX),
    f_deleted                      CHAR(1))

ALTER TABLE vis_image
ADD PRIMARY KEY (f_image_id)

-- �������� vis_image
CREATE TABLE vis_image_document
    (f_img_doc_id                  int NOT NULL,
    f_image_id                     int,
    f_doc_id                       int,
    f_deleted                      CHAR(1))

ALTER TABLE vis_image_document
ADD PRIMARY KEY (f_img_doc_id)


