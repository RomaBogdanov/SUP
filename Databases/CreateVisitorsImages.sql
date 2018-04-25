-- Создание БД VisitorsImages на T-SQL
create database VisitorsImages;
go

use VisitorsImages;
go

-- Создание vis_image
CREATE TABLE vis_image
    (f_image_id                    UNIQUEIDENTIFIER NOT NULL,
	f_visitor_id                   int,
	f_image_type                   int,
	f_data                         VARBINARY(MAX))

ALTER TABLE vis_image
ADD PRIMARY KEY (f_image_id)

