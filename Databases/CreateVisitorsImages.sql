-- �������� �� VisitorsImages �� T-SQL
create database VisitorsImages;
go

use VisitorsImages;
go

-- �������� vis_image
CREATE TABLE vis_image
    (f_image_id                    UNIQUEIDENTIFIER NOT NULL,
	f_visitor_id                   int,
	f_image_type                   int,
	f_data                         VARBINARY(MAX))

ALTER TABLE vis_image
ADD PRIMARY KEY (f_image_id)


/*-- �������� �� VisitorsImages �� T-SQL
create database VisitorsImages;
go

alter database VisitorsImages
add filegroup fsGroup contains FILESTREAM;
go

alter database VisitorsImages
add file
  ( NAME = 'fsVisitorsImages', FILENAME = 'c:\<your_file_path>'
   )
to filegroup fsGroup;
go

use VisitorsImages;
go

-- �������� vis_image
CREATE TABLE vis_image
    (f_image_id                    UNIQUEIDENTIFIER NOT NULL,
	f_visitor_id                   int,
	f_image_type                   int,
	f_data                         VARBINARY(MAX) FILESTREAM)

ALTER TABLE vis_image
ADD PRIMARY KEY (f_image_id)
*/