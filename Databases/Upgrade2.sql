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

/*
-- �������� vis_access_points
-- ������� � ����������� �� �������� "����� �������". ����� ������� ��������� � �������� "�����",
-- � ����� ���� �� ���������. ����� ������� �������������, �.�. �������� ���������������� ������.

if OBJECT_ID('vis_access_points') is not null
	drop table vis_access_points

create table vis_access_points
(
	f_access_point_id              int not null,
	f_access_point_name            varchar(128),
	f_access_point_description     varchar(200),
	f_access_point_space_in_id_hi  int,
	f_access_point_space_in_id_lo  int,
	f_access_point_space_out_id_hi int,
	f_access_point_space_out_id_lo int,
	f_access_point_space_in        varchar(128), -- ? ����� �� ����������� ���������. ���� ���������, ��� �����������.
	f_access_point_space_out       varchar(128), -- ? ����� �� �������� ���������. ���� ���������, ��� �����������.
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
	f_access_point_controller      varchar(16),
	f_access_point_path            varchar(MAX),
	f_access_point_data            varchar(MAX)   -- ����� ����� ����� ��������� ��������������� ������ �� Andover
)

alter table vis_access_points
	add primary key(f_access_point_id)

if not exists(select * from vis_access_points where f_access_point_id='0')
begin
	insert into vis_access_points values ('0', '', '', 0, 0, 0, 0, '', '', 'N', '', '', '0', '0', '', '', '')
end
go
*/

/*
-- �������� vis_areas
-- ������� � ����������� �� �������� "������� �������". ������ ������ ��������� � zones

if OBJECT_ID('vis_areas') is not null
	drop table vis_areas;

create table vis_areas
(
	f_area_id                      int not null,
	f_area_name                    varchar(128),
	f_area_descript                varchar(200),
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
	f_area_controller              varchar(16),
	f_area_path                    varchar(MAX),
	f_area_data                    varchar(MAX)   -- ����� ����� ����� ��������� ��������������� ������ �� Andover
)

alter table vis_areas
	add primary key (f_area_id)

if not exists(select * from vis_areas where f_area_id='0')
begin
	insert into vis_areas values ('0', '', '', 'N', '', '', '0', '0', '', '', '')
end
go
*/

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

/*
-- �������� vis_schedules

if OBJECT_ID('vis_schedules') is not null
	drop table vis_schedules

create table vis_schedules
(
	f_schedule_id                  int not null,
    f_schedule_name                VARCHAR(128),
	f_schedule_description         varchar(200),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
    f_schedule_controller          varchar(16),
    f_schedule_path                varchar(MAX),
    f_schedule_data                varchar(MAX)   -- ����� ����� ����� ��������� ��������������� ������ �� Andover
)

alter table vis_schedules
	add primary key (f_schedule_id)

if not exists (select * from vis_schedules where f_schedule_id='0')
begin
	insert into vis_schedules values('0', '', '', 'N', '', '0', '0', '0', '', '', '')
end
go
*/
-- �������� vis_space
-- ������� ������ ���������. ��� ���������� ���������� ������� ������������
-- "�������" (��. ������� vis_doors). ������ �� ����� �������� "��������".

/*
if OBJECT_ID('vis_spaces') is not null
	drop table vis_spaces

CREATE TABLE vis_spaces
	(
	f_space_id int not null,
	f_num_real varchar(50), --����� ��������� ��� � ��������� ���� ��� �������� ���������, � ������ ����� ������������ �������� ���������
	f_num_build varchar(50), --����� ��������� ��� � ��������� ���� ��� �������� ���������, � ������ ����� ������������ �������� ��������� �� ����� �������������
	f_descript varchar(200), -- �������� ���������
	f_note varchar(200), -- ���������� �� ���������
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int
	)

ALTER TABLE vis_spaces
ADD PRIMARY KEY (f_space_id)

if not exists (select * from vis_spaces where f_space_id='0')
begin
	insert into vis_spaces values ('0', '', '', '', '', 'N', '', '')
end
go
*/
