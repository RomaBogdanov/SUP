use Visitors;
go


-- �������� vis_access_level

if	OBJECT_ID('vis_access_level') is not null
	drop table vis_access_level;

create table vis_access_level
(
	f_access_level_id int not null,
	f_level_name varchar(50), -- �������� ������ �������
	f_area_id_hi int, -- id ������� �������
	f_area_id_lo int, -- id ������� �������
	f_schedule_id_hi int, -- id ����������
	f_schedule_id_lo int, -- id ����������
	f_access_level_note varchar(100), -- ������� �� ������ �������
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
)

alter table vis_access_level
	add primary key(f_access_level_id)

if not exists(select * from vis_access_level where f_access_level_id = '0')
begin
	insert into vis_access_level values ('0', '', '', '', '', '', '', 'N', '', '')
end

-- �������� vis_access_points_ext
-- ������� � ����������� �� �������� "����� �������". �������������� ������.

if OBJECT_ID('vis_access_points_ext') is not null
	drop table vis_access_points_ext

create table vis_access_points_ext
(
	f_access_point_id              int not null,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
	f_description                  varchar(MAX)
)

alter table vis_access_points_ext
	add primary key(f_access_point_id)

	
-- �������� vis_areas_ext
-- ������� � ����������� �� �������� "������� �������". �������������� ������.

if OBJECT_ID('vis_areas_ext') is not null
	drop table vis_areas_ext;

create table vis_areas_ext
(
    f_area_id                      int not null,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
	f_description                  varchar(MAX)
)

alter table vis_areas_ext
	add primary key (f_area_id)
go

-- �������� vis_areas_order_elements
-- ������� ����������� ����� �������� � ��������� �������.

if OBJECT_ID('vis_areas_order_elements') is not null
	drop table vis_areas_order_elements;

create table vis_areas_order_elements
(
	f_area_order_element_id int not null,
	f_oe_id int, -- id �������� ������
	f_area_id_hi int, -- id ������� �������
	f_area_id_lo int, -- id ������� �������
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
)

alter table vis_areas_order_elements
	add primary key (f_area_order_element_id)

if not exists(select * from vis_areas_order_elements where f_area_order_element_id='0')
begin
	insert into vis_areas_order_elements values ('0','0','0','0','N','','')
end
go

-- �������� vis_areas_spaces
-- ������� ����������� ����� "��������� �������" � "�����������". 

if OBJECT_ID('vis_areas_spaces') is not null
	drop table vis_areas_spaces;

create table vis_areas_spaces
(
	f_area_space_id int not null,
	f_area_id_hi int, -- id ������� �������
	f_area_id_lo int, -- id ������� �������
	f_space_id int, -- id ���������
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
)

alter table vis_areas_spaces
	add primary key (f_area_space_id)

if not exists(select * from vis_areas_spaces where f_area_space_id = '0')
begin
	insert into vis_areas_spaces values ('0', '', '', '', 'N', '', '')
end
go


-- �������� vis_card_area
-- ������� ����� ����� ������� ������� � ��������� �������

if object_id('vis_card_area') is not null
	drop table vis_card_area;

create table vis_card_area
(
	f_ca_id int not null,
	f_card_id_hi int, -- id �����
	f_card_id_lo int, -- id �����
	f_area_id_hi int, -- id ������� �������
	f_area_id_lo int, -- id ������� �������
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int
)

alter table vis_card_area
add primary key (f_ca_id)

if not exists(select * from vis_card_area where f_ca_id = '0')
begin
	insert into vis_card_area values ('0', '0', '0', '0', '0', 'N', '', '')
end
go


-- �������� vis_doors
-- ������� �������� �������� "�����". "�����" - ����������� ����� "�����������", 
-- � �������� ���� ��� ������������ ���� ��� �����������.

if OBJECT_ID('vis_doors') is not null
	drop table vis_doors;

CREATE TABLE vis_doors
(
	f_door_id int not null,
	f_door_num varchar(20), -- ����� "�����". ��� � ��������� ���� ��� ���������.
	f_descript varchar(100), -- �������� "�����". �� ��������� ����� �������� ����������� "���������"
	f_space_in int, -- id ����������� ���������.
	f_space_out int, -- id �������� ���������.
	f_access_point_id_hi int, -- id ����� �������.
	f_access_point_id_lo int, -- id ����� �������.
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int
)

alter table vis_doors
add primary key (f_door_id)

if not exists (select * from vis_doors where f_door_id = '0')
begin
	insert into vis_doors values ('0', '', '', '', '', '', '', 'N', '', '')
end
go


-- �������� vis_schedules_ext
-- ������� � ����������� �� �������� "����������". �������������� ������.

if OBJECT_ID('vis_schedules_ext') is not null
	drop table vis_schedules_ext

create table vis_schedules_ext
(
	f_schedule_id                  int not null,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
    f_description                  varchar(MAX)
)

alter table vis_schedules_ext
	add primary key (f_schedule_id)
go

