-- � 04.08.2018 ��� ��������� � �� ������ ���������� � ���� ������
-- ����� ���� ����������� "��������" ���� �� ����������, �� ������������ ��

-- ������ ���������� ��� ������.


-- �������� vis_templates
-- ������� ������ ��������.

use Visitors;
go

if OBJECT_ID('vis_templates') is not null
	drop table vis_templates;

CREATE TABLE vis_templates
    (f_template_id                 int NOT NULL,
	f_template_type                int,
    f_template_name                nvarchar(50),
	f_template_description         nvarchar(200),
	f_template_areas               nvarchar(MAX),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_templates
ADD PRIMARY KEY (f_template_id)

if not exists(select * from vis_templates where f_template_id = '0')
begin
	insert into vis_templates values ('0', '0', '', '', '', 'N', '', '0')
end
go


-- �������� ������� vis_areas_order_elements

use Visitors;
go

if OBJECT_ID('vis_areas_order_elements') is not null
	drop table vis_areas_order_elements;
go


-- ���������� ������� vis_order_elements

use Visitors;
go

ALTER TABLE vis_order_elements
ADD
	f_oe_templates                 nvarchar(MAX),
	f_oe_areas                     nvarchar(MAX),
	f_schedule_id                  int
go

UPDATE vis_order_elements SET f_oe_templates='', f_oe_areas='', f_schedule_id=0
go

