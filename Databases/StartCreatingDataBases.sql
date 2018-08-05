/*EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Visitors'
GO

USE master
GO

ALTER DATABASE Visitors SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO

DROP DATABASE Visitors


EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'VisitorsImages'
GO

USE master
GO

ALTER DATABASE VisitorsImages SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO

DROP DATABASE VisitorsImages


 EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'VisitorsLogs'
GO

USE master
GO

ALTER DATABASE VisitorsLogs SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO

DROP DATABASE VisitorsLogs*/



-- Файл создания Баз данных.

-- Пакет названий для создания Баз Данных

use master;
go

-- Создание БД Visitors на T-SQL
-- ===========================================

if DB_ID('Visitors') is not null
	drop database Visitors;
create database Visitors
COLLATE Cyrillic_General_CI_AS
go

use Visitors;
go

-- Создание vis_access_level

if	OBJECT_ID('vis_access_level') is not null
	drop table vis_access_level;

create table vis_access_level
(
	f_access_level_id int not null,
	f_level_name nvarchar(50), -- название уровня доступа
	f_area_id_hi int, -- id области доступа
	f_area_id_lo int, -- id области доступа
	f_schedule_id_hi int, -- id расписания
	f_schedule_id_lo int, -- id расписания
	f_access_level_note nvarchar(100), -- заметка по уровню доступа
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

-- Создание vis_access_points
-- Таблица с информацией по сущности "точка доступа". Точка доступа привязана к сущности "дверь",
-- а может быть не привязана. Точка доступа двунаправлена, т.е. возможен однонаправленный проход.

if OBJECT_ID('vis_access_points') is not null
	drop table vis_access_points

create table vis_access_points
(
	f_access_point_id              int not null,
	f_access_point_name            nvarchar(128),
	f_access_point_description     nvarchar(200),
	f_access_point_space_in_id_hi  int,
	f_access_point_space_in_id_lo  int,
	f_access_point_space_out_id_hi int,
	f_access_point_space_out_id_lo int,
	f_access_point_space_in        nvarchar(128), -- ? метка по внутреннему помещению. Пока непонятно, что привязывать.
	f_access_point_space_out       nvarchar(128), -- ? метка по внешнему помещению. Пока непонятно, что привязывать.
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
	f_access_point_controller      nvarchar(16),
	f_access_point_path            nvarchar(MAX),
	f_access_point_data            nvarchar(MAX)   -- Здесь можно будет сохранять сериализованный объект из Andover
)

alter table vis_access_points
	add primary key(f_access_point_id)

if not exists(select * from vis_access_points where f_access_point_id='0')
begin
	insert into vis_access_points values ('0', '', '', 0, 0, 0, 0, '', '', 'N', '', '', '0', '0', '', '', '')
end
go


-- Создание vis_access_points_ext
-- Таблица с информацией по сущности "точки доступа". Дополнительные данные.

if OBJECT_ID('vis_access_points_ext') is not null
	drop table vis_access_points_ext

create table vis_access_points_ext
(
	f_access_point_id              int not null,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
	f_description                  nvarchar(MAX)
)

alter table vis_access_points_ext
	add primary key(f_access_point_id)
go
	

-- Создание vis_areas
-- Таблица с информацией по сущности "области доступа". Аналог таблиц связанных с zones

if OBJECT_ID('vis_areas') is not null
	drop table vis_areas;

create table vis_areas
(
	f_area_id                      int not null,
	f_area_name                    nvarchar(128),
	f_area_descript                nvarchar(200),
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
	f_area_controller              nvarchar(16),
	f_area_path                    nvarchar(MAX),
	f_area_data                    nvarchar(MAX)   -- Здесь можно будет сохранять сериализованный объект из Andover
)

alter table vis_areas
	add primary key (f_area_id)

if not exists(select * from vis_areas where f_area_id='0')
begin
	insert into vis_areas values ('0', '', '', 'N', '', '', '0', '0', '', '', '')
end
go

	
-- Создание vis_areas_ext
-- Таблица с информацией по сущности "области доступа". Дополнительные данные.

if OBJECT_ID('vis_areas_ext') is not null
	drop table vis_areas_ext;

create table vis_areas_ext
(
    f_area_id                      int not null,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
	f_description                  nvarchar(MAX)
)

alter table vis_areas_ext
	add primary key (f_area_id)
go


-- Создание vis_areas_spaces
-- Таблица соотношения между "областями доступа" и "помещениями". 

if OBJECT_ID('vis_areas_spaces') is not null
	drop table vis_areas_spaces;

create table vis_areas_spaces
(
	f_area_space_id int not null,
	f_area_id_hi int, -- id области доступа
	f_area_id_lo int, -- id области доступа
	f_space_id int, -- id помещения
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

-- Создание vis_areas_order_elements
-- Таблица соотношения между заявками и областями доступа.

if OBJECT_ID('vis_areas_order_elements') is not null
	drop table vis_areas_order_elements;

create table vis_areas_order_elements
(
	f_area_order_element_id int not null,
	f_oe_id int, -- id элемента заявки
	f_area_id_hi int, -- id области доступа
	f_area_id_lo int, -- id области доступа
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


-- Создание vis_cabinets
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_cabinets') is not null
	drop table vis_cabinets;

CREATE TABLE vis_cabinets
    (f_cabinet_id                  int NOT NULL,
    f_cabinet_num                  nvarchar(50),
    f_cabinet_desc                 nvarchar(200),
    f_door_num                     nvarchar(20),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_door_num_int                 int)

ALTER TABLE vis_cabinets
ADD PRIMARY KEY (f_cabinet_id)

if not exists(select * from vis_cabinets where f_cabinet_id = '0')
begin
	insert into vis_cabinets values ( '0', '', '', '', 'N', '', '0', '0')
end
go

-- Создание vis_cabinets_zones
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_cabinets_zones') is not null
	drop table vis_cabinets_zones;

CREATE TABLE vis_cabinets_zones
    (f_cabinet_id                  int,
    f_zone_id                      int,
    f_cabinet_zone_id              int NOT NULL)

ALTER TABLE vis_cabinets_zones
ADD CONSTRAINT pk_cabinets_zones PRIMARY KEY (f_cabinet_zone_id)

if not exists(select * from vis_cabinets_zones where f_cabinet_zone_id = '0')
begin
	insert into vis_cabinets_zones values ( '0', '0', '0')
end
go

-- Создание vis_cabinets_zones_2
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_cabinets_zones_2') is not null
	drop table vis_cabinets_zones_2;

CREATE TABLE vis_cabinets_zones_2
    (f_cabinet_id                  int,
    f_zone_id                      int,
    f_cabinet_zone_id              int NOT NULL)

ALTER TABLE vis_cabinets_zones_2
ADD CONSTRAINT pk_cabinets_zones_2 PRIMARY KEY (f_cabinet_zone_id)

if not exists(select * from vis_cabinets_zones_2 where f_cabinet_zone_id = '0')
begin
	insert into vis_cabinets_zones_2 values ( '0', '0', '0')
end
go

-- Создание vis_cards
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_cards') is not null
	drop table vis_cards;

CREATE TABLE vis_cards
    (f_card_id                     int NOT NULL,
    f_state_id                     int,
	f_card_name                    nvarchar(128),
    f_card_text                    nvarchar(200),
    f_last_visit_id                int,
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_create_date                  DATE,
    f_lost_date                    DATE,
    f_comment                      nvarchar(200),
    f_card_num                     int,
	f_object_id_hi                 int,
    f_object_id_lo                 int,
    f_card_controller              nvarchar(16),
    f_card_path                    nvarchar(MAX),
    f_card_data                    nvarchar(MAX)   -- Здесь можно будет сохранять сериализованный объект из Andover
)

ALTER TABLE vis_cards
ADD PRIMARY KEY (f_card_id)

if not exists(select * from vis_cards where f_card_id = '0')
begin
	insert into vis_cards values ( '0', '0', '', '', '0', 'N', '', '0', '', '', '', '0', '0', '0', '', '', '')
end
go



-- Создание vis_card_area
-- Таблица связи между картами доступа и областями доступа

if object_id('vis_card_area') is not null
	drop table vis_card_area;

create table vis_card_area
(
	f_ca_id int not null,
	f_card_id_hi int, -- id карты
	f_card_id_lo int, -- id карты
	f_area_id_hi int, -- id области доступа
	f_area_id_lo int, -- id области доступа
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

-- Создание vis_cars
-- Таблица списка машин.

if OBJECT_ID('vis_cars') is not null
	drop table vis_cars;

create table vis_cars
(
	f_car_id int not null,
	f_car_mark nvarchar(50), -- марка машины
	f_car_number nvarchar(20), -- номер машины
	f_org_id int, -- номер организации
	f_visitor_id int, -- номер водителя
	f_color nvarchar(25), -- цвет машины
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int
)

alter table vis_cars
add primary key (f_car_id)

if not exists(select * from vis_cars where f_car_id = '0')
begin
	insert into vis_cars values ('0', '', '', '', '', '', 'N', '', '')
end
go

-- Создание vis_countries
-- Таблица списка стран. Пока используется для определения гражданств и стран из которых организации.

if OBJECT_ID('vis_countries') is not null
	drop table vis_countries;

CREATE TABLE vis_countries
    (f_cntr_id                     int NOT NULL,
    f_cntr_name                    nvarchar(50),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_countries
ADD PRIMARY KEY (f_cntr_id)

if not exists(select * from vis_countries where f_cntr_id = '0')
begin
	insert into vis_countries values ( '0', '', 'N', '', '0')
end
go

-- Создание vis_departaments
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_departaments') is not null
	drop table vis_departaments;

CREATE TABLE vis_departaments
    (f_dep_id                      int NOT NULL,
    f_org_id                       int,
    f_dep_name                     nvarchar(100),
    f_short_dep_name               nvarchar(15),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_parent_id                    int)

ALTER TABLE vis_departaments
ADD PRIMARY KEY (f_dep_id)

if not exists(select * from vis_departaments where f_dep_id = '0')
begin
	insert into vis_departaments values ( '0', '0', '', '', 'N', '', '0', '-1')
end
go

-- Создание vis_documents
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_documents') is not null
	drop table vis_documents;

CREATE TABLE vis_documents
    (f_doc_id                      int NOT NULL,
    f_doc_name                     nvarchar(40),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_documents
ADD PRIMARY KEY (f_doc_id)

if not exists(select * from vis_documents where f_doc_id = '0')
begin
	insert into vis_documents values ( '0', '', 'N', '', '0')
end
go

-- Создание vis_doors
-- Таблица описания сущности "дверь". "Дверь" - разделитель между "Помещениями", 
-- у которого есть или обыкновенный ключ или электронный.

if OBJECT_ID('vis_doors') is not null
	drop table vis_doors;

CREATE TABLE vis_doors
(
	f_door_id int not null,
	f_door_num nvarchar(20), -- номер "двери". Дан в текстовом виде для обобщения.
	f_descript nvarchar(100), -- описание "двери". По умолчанию равно описанию внутреннего "помещения"
	f_space_in int, -- id внутреннего помещения.
	f_space_out int, -- id внешнего помещения.
	f_access_point_id_hi int, -- id точки доступа.
	f_access_point_id_lo int, -- id точки доступа.
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

-- Создание vis_flag
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_flag') is not null
	drop table vis_flag;

CREATE TABLE vis_flag
    (f_user_id                     int,
    f_modified                     nvarchar(1))
	
if not exists(select * from vis_flag where f_user_id = '0')
begin
	insert into vis_flag values ( '0', 'N')
end
go

-- Создание vis_key_case
-- Таблица с описанием штырей. Штырь и пенал - одно и тоже.

if OBJECT_ID('vis_key_case') is not null
	drop table vis_key_case

create table vis_key_case
(
	f_key_case_id int not null,
	f_inner_code nvarchar(25), -- внутренний код штыря
	f_key_holder_num nvarchar(25), -- номер ключницы
	f_cell_num int, -- номер ячейки
	f_descript nvarchar(200), -- описание
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int
)

alter table vis_key_case
	add primary key(f_key_case_id)

if not exists(select * from vis_key_case where f_key_case_id='0')
begin
	insert into vis_key_case values ('0', '', '', '', '', 'N', '', '')
end

-- Создание vis_key_holder
-- Таблица с описанием ключниц.

if OBJECT_ID('vis_key_holder') is not null
	drop table vis_key_holder

create table vis_key_holder
(
	f_key_holder_id int not null,
	f_key_holder_num nvarchar(25), -- номер ключницы
	f_descript nvarchar(200), -- описание
	f_count int, -- количество ячеек в ключнице
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int
)

alter table vis_key_holder
	add primary key(f_key_holder_id)

if not exists(select * from vis_key_holder where f_key_holder_id='0')
begin
	insert into vis_key_holder values ('0', '', '', '', 'N', '', '')
end

-- Создание vis_keys
-- Таблица с описанием ключа. Доработать (?)

if OBJECT_ID('vis_keys') is not null
	drop table vis_keys;

create table vis_keys
(
	f_key_id int not null,
	f_key_name nvarchar(20),
	f_key_description nvarchar(100),
	f_door_id int, -- номер двери, к которой привязан ключ
	f_key_holder_id int, -- номер ключницы, в которой ключ
	f_key_case_id int, -- номер пенала, в котором ключ
	f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int
)

alter table vis_keys
	add primary key(f_key_id)

if not exists(select * from vis_keys where f_key_id='0')
begin
	insert into vis_keys values ('0', '', '', '', '', '', 'N', '', '')
end
go

-- Создание vis_roles
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_roles') is not null
	drop table vis_roles;

CREATE TABLE vis_roles
    (f_role_id                     int,
    f_role                         nvarchar(100),
    f_grb_id                       int)

if not exists(select * from vis_roles where f_role_id = '0')
begin
	insert into vis_roles values ( '0', '', '0')
end
go

-- Создание vis_role_lists
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_role_lists') is not null
	drop table vis_role_lists;

CREATE TABLE vis_role_lists
    (f_id                          int,
    f_role_id                      int,
    f_user_id                      int,
    f_status_id                    int)

if not exists(select * from vis_role_lists where f_id = '0')
begin
	insert into vis_role_lists values ( '0', '0', '0', '0')
end
go

-- Создание vis_users
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_users') is not null
	drop table vis_users;

CREATE TABLE vis_users
    (f_user_id                     int,
    f_user                         nvarchar(100),
    f_pass                         nvarchar(100),
	f_timeout                      int)

if not exists(select * from vis_users where f_user_id = '0')
begin
	insert into vis_users values ( '0', '', '', '0')
end
go

if not exists(select * from vis_users where f_user_id = '1')
begin
	insert into vis_users values ( '1', '1', '1', '180')
end
go

-- Создание vis_order_elements
-- Одна из основных таблиц.
-- Даёт расширенную информацию по Заявкам. Идёт по связи один ко многим с vis_orders.

if OBJECT_ID('vis_order_elements') is not null
	drop table vis_order_elements;

CREATE TABLE vis_order_elements
    (f_oe_id                       int NOT NULL,
    f_ord_id                       int,
    f_visitor_id                   int,
    f_catcher_id                   int,
    f_time_from                    DATETIME,
    f_time_to                      DATETIME,
    f_passes                       nvarchar(1000),
    f_disabled                     nvarchar(1),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_not_remaind                  nvarchar(1),
    f_full_role                    nvarchar(1),
    f_other_org                    nvarchar(200),
    f_org_id                       INT,--организация
    f_position                     NVARCHAR(200)--должность        
  )

ALTER TABLE vis_order_elements
ADD PRIMARY KEY (f_oe_id)

if not exists(select * from vis_order_elements where f_oe_id = '0')
begin
	insert into vis_order_elements values ( '0', '', '', '', '', '', '', '', 'N', '', '', '', '', '',0,'')
end
go

-- Создание vis_orderlist
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_orderlist') is not null
	drop table vis_orderlist;

CREATE TABLE vis_orderlist
    (f_ord_id                      int,
    f_visit_id                     int,
    f_deleted                      CHAR(1),
    f_orderlist_id                 int NOT NULL)

ALTER TABLE vis_orderlist
ADD CONSTRAINT orderlist_idx_un PRIMARY KEY (f_orderlist_id)

if not exists(select * from vis_orderlist where f_ord_id = '0')
begin
	insert into vis_orderlist values ( '', '', 'N', '0')
end
go

-- Создание vis_orders
-- Одна из основных таблиц.
-- Содержит список Заявок.

if OBJECT_ID('vis_orders') is not null
	drop table vis_orders;

CREATE TABLE vis_orders
    (f_ord_id                      int NOT NULL,
    f_order_type_id                int NOT NULL,
    f_reg_number                   int,
    f_ord_date                     DATE,
    f_date_from                    DATE,
    f_date_to                      DATE,
    f_signed_by                    int,
    f_adjusted_with                int,
    f_notes                        nvarchar(150),
    f_disabled                     nvarchar(1),
    f_deleted                      CHAR(1),
    f_rec_date                     DATETIME,--время последнего редактирования заявки
    f_rec_operator                 int,--id оператора который последним внес изменеия в заявку
    f_temp_posted                  nvarchar(1),
    f_new_rec_date                 DATETIME,--время создания заявки
    f_new_rec_operator             INT,--id оператора создателя заявки
    f_barcode                      NVARCHAR(200),--штрихкод СЭД(система электронного документооборота)
    f_image_id                     INT--ссылка на изображение
  )

ALTER TABLE vis_orders
ADD PRIMARY KEY (f_ord_id)

if not exists(select * from vis_orders where f_ord_id = '0')
begin
	insert into vis_orders values ( '0', '', '', '', '', '', '', '', '', '', 'N', '', '', '','','','','')
end
go

-- Создание vis_organizations
-- Важная таблица, список всех организаций используемых программой

if OBJECT_ID('vis_organizations') is not null
	drop table vis_organizations;

CREATE TABLE vis_organizations
    (f_org_id                      int NOT NULL,
    f_org_type                     nvarchar(15),
    f_org_name                     nvarchar(50),
    f_has_free_access              nvarchar(1),
    f_is_master                    nvarchar(1),
    f_is_basic                     nvarchar(1),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_syn_id                       int,
    f_comment                      nvarchar(200),
    f_full_org_name                nvarchar(70),
    f_cntr_id                      int,
    f_region_id                    int)

ALTER TABLE vis_organizations
ADD PRIMARY KEY (f_org_id)

if not exists(select * from vis_organizations where f_org_id = '0')
begin
	insert into vis_organizations values ( '0', '', '', '', '', '', 'N', '', '', '', '', '', '', '')
end
go

-- Создание vis_regions
-- Нововведённая таблица. Необходима для получения данных по регионам привязанным к странам.

if OBJECT_ID('vis_regions') is not null
	drop table vis_regions

CREATE TABLE vis_regions
    (f_region_id                   int NOT NULL,
    f_region_name                  nvarchar(50),
    f_cntr_id                      int,
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_regions
ADD PRIMARY KEY (f_region_id)

if not exists(select * from vis_regions where f_region_id = '0')
begin
	insert into vis_regions values ( '0', '', '0', 'N', '', '')
end
go

-- Создание vis_schedules

create table vis_schedules
(
	f_schedule_id                  int not null,
    f_schedule_name                VARCHAR(128),
	f_schedule_description         nvarchar(200),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
    f_schedule_controller          nvarchar(16),
    f_schedule_path                nvarchar(MAX),
    f_schedule_data                nvarchar(MAX)   -- Здесь можно будет сохранять сериализованный объект из Andover
)

alter table vis_schedules
	add primary key (f_schedule_id)

if not exists (select * from vis_schedules where f_schedule_id='0')
begin
	insert into vis_schedules values('0', '', '', 'N', '', '0', '0', '0', '', '', '')
end
go


-- Создание vis_schedules_ext
-- Таблица с информацией по сущности "расписания". Дополнительные данные.

if OBJECT_ID('vis_schedules_ext') is not null
	drop table vis_schedules_ext

create table vis_schedules_ext
(
	f_schedule_id                  int not null,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
    f_description                  nvarchar(MAX)
)

alter table vis_schedules_ext
	add primary key (f_schedule_id)
go


-- Создание vis_space
-- Таблица списка помещений. Под помещением понимается область ограниченная
-- "дверями" (см. таблицу vis_doors). Пришла на смену сущности "кабинеты".

if OBJECT_ID('vis_spaces') is not null
	drop table vis_spaces

CREATE TABLE vis_spaces
	(
	f_space_id int not null,
	f_num_real nvarchar(50), --номер помещения дан в текстовом виде для большего обобщения, в теории будет эквивалентем названию помещения
	f_num_build nvarchar(50), --номер помещения дан в текстовом виде для большего обобщения, в теории будет эквивалентем названию помещения на этапе строительства
	f_descript nvarchar(200), -- описание помещения
	f_note nvarchar(200), -- примечание по помещению
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

-- Создание vis_spr_cardstates
-- Таблица статусов пропусков.

if OBJECT_ID('vis_spr_cardstates') is not null
	drop table vis_spr_cardstates

CREATE TABLE vis_spr_cardstates
    (f_state_id                    int NOT NULL,
    f_state_text                   nvarchar(50),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_spr_cardstates
ADD PRIMARY KEY (f_state_id)

if not exists(select * from vis_spr_cardstates where f_state_id = '0')
begin
	insert into vis_spr_cardstates values ( '0', '', 'N', '', '')
end
go

-- на данные строки завязан код клиентских приложений, поэтому не удалять и учитывать
-- при работе

insert into vis_spr_cardstates values ( '1', 'Активен', 'N', '20030711 14:41:24', '-1')
insert into vis_spr_cardstates values ( '3', 'Выдан', 'N', '20030711 14:41:25', '-1')
insert into vis_spr_cardstates values ( '4', 'Утерян', 'N', '20030711 14:41:28', '-1')
insert into vis_spr_cardstates values ( '2', 'Неактивен', 'N', '20030711 14:41:58', '-1')

-- Создание vis_spr_order_types
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_spr_order_types') is not null
	drop table vis_spr_order_types

CREATE TABLE vis_spr_order_types
    (f_order_type_id               int NOT NULL,
    f_order_text                   nvarchar(50),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_spr_order_types
ADD PRIMARY KEY (f_order_type_id)

if not exists(select * from vis_spr_order_types where f_order_type_id = '0')
begin
	insert into vis_spr_order_types values ( '0', '', 'N', '', '')
end
go

-- на данные строки завязан код клиентских приложений, поэтому не удалять и учитывать
-- при работе

insert into vis_spr_order_types values ( '1', 'Разовая', 'N', '20030711 14:39:56', '-1')
insert into vis_spr_order_types values ( '2', 'Временная', 'N', '20030711 14:39:56', '-1')
insert into vis_spr_order_types values ( '3', 'Бессрочная', 'N', '20030711 14:39:56', '-1')
insert into vis_spr_order_types values ( '4', 'На основании', 'N', '20030711 14:39:57', '-1')


-- Создание vis_templates
-- Таблица списка шаблонов.

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


-- Создание vis_visitors
-- Одна из основных таблиц.
-- Таблица посетителей и сотрудников Транснефти.

if OBJECT_ID('vis_visitors') is not null
	drop table vis_visitors

CREATE TABLE vis_visitors
    (f_visitor_id                  int NOT NULL,
    f_doc_id                       int,
    f_cntr_id                      int,
    f_family                       nvarchar(50),
    f_fst_name                     nvarchar(20),
    f_sec_name                     nvarchar(20),
    f_birth_date                   DATE,
    f_doc_seria                    nvarchar(20),
    f_doc_num                      nvarchar(20),
    f_doc_date                     DATE,
    f_doc_org                      nvarchar(100),
    f_phones                       nvarchar(50),
    f_vr_text                      nvarchar(100),
    f_is_short_data                nvarchar(1),
    f_dep_id                       int,
    f_job                          nvarchar(50),
    f_can_sign_orders              nvarchar(1),
    f_can_adjust_orders            nvarchar(1),
    f_can_have_visitors            nvarchar(1),
    f_persona_non_grata            nvarchar(1),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_org_id                       int,
    f_rec_date_pass                DATE,
    f_rec_operator_pass            int,
    f_cabinet_id                   int,
    f_full_name                    nvarchar(100),
    f_personal_data_agreement      CHAR(1),
    f_personal_data_last_date      DATE,
    f_doc_code                     nvarchar(20))

ALTER TABLE vis_visitors
ADD PRIMARY KEY (f_visitor_id)

if not exists(select * from vis_visitors where f_visitor_id = '0')
begin
	insert into vis_visitors values ( '0', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'N', '', '', '', '', '', '', '', '', '', '')
end
go

-- Создание vis_visitors_documents
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_visitors_documents') is not null
	drop table vis_visitors_documents;

CREATE TABLE vis_visitors_documents
    (f_vd_id                       int NOT NULL,
    f_visitor_id                   int,
    f_doctype_id                   int,
    f_doc_name                     nvarchar(100),
    f_doc_seria                    nvarchar(20),
    f_doc_num                      nvarchar(20),
    f_doc_date                     DATE,
	f_doc_date_to                  DATE,
    f_doc_org                      nvarchar(100),
    f_doc_code                     nvarchar(20),
	f_birth_date                   DATE,
	f_comment                      nvarchar(100),
    f_deleted                      CHAR(1))

ALTER TABLE vis_visitors_documents
ADD PRIMARY KEY (f_vd_id)

if not exists(select * from vis_visitors_documents where f_vd_id = '0')
begin
	insert into vis_visitors_documents values ( '0', '', '', '', '', '', '', '', '', '', '', '', '')
end
go

-- Создание vis_visits
-- TODO: Сделать описание таблицы


if OBJECT_ID('vis_visits') is not null
	drop table vis_visits;

CREATE TABLE vis_visits
    (f_visit_id                    int NOT NULL,
    f_card_id_hi                   int,
	f_card_id_lo                   int,
    f_visitor_id                   int,
    f_time_out                     DATE,
    f_time_in                      DATE,
    f_visit_text                   nvarchar(200),
    f_date_from                    DATE,
    f_date_to                      DATE,
    f_order_id                     int,
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_reason                       nvarchar(1000),
    f_rec_operator_back            int,
    f_rec_date_back                DATE,
    f_card_status                  nvarchar(1),
    f_eff_zonen_text               nvarchar(1000))

ALTER TABLE vis_visits
ADD PRIMARY KEY (f_visit_id)

if not exists(select * from vis_visits where f_visit_id = '0')
begin
	insert into vis_visits values ( '0', '', '', '', '', '', '', '', '', '', 'N', '', '', '', '', '', '', '')
end
go

-- Создание vis_zone_types
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_zone_types') is not null
	drop table vis_zone_types;

CREATE TABLE vis_zone_types
    (f_zone_type_id                int NOT NULL,
    f_zone_type_name               nvarchar(50) NOT NULL,
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_comment                      nvarchar(200))

ALTER TABLE vis_zone_types
ADD PRIMARY KEY (f_zone_type_id)

if not exists(select * from vis_zone_types where f_zone_type_id = '0')
begin
	insert into vis_zone_types values ( '0', '', 'N', '', '', '')
end
go

-- Создание vis_zones
-- TODO: Сделать описание таблицы
-- Полностью переделать. Никуда не годится.

if OBJECT_ID('vis_zones') is not null
	drop table vis_zones;

CREATE TABLE vis_zones
    (f_zone_id                     int NOT NULL,
    f_zone_type_id                 int,
    f_zone_name                    CHAR(100),
    f_comment                      nvarchar(200),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_zone_num                     int,
    f_monday_time_beg_1            DATE,
    f_monday_time_beg_2            DATE,
    f_monday_time_beg_3            DATE,
    f_monday_time_beg_4            DATE,
    f_monday_time_end_1            DATE,
    f_monday_time_end_2            DATE,
    f_monday_time_end_3            DATE,
    f_monday_time_end_4            DATE,
    f_tuesday_time_beg_1           DATE,
    f_tuesday_time_beg_2           DATE,
    f_tuesday_time_beg_3           DATE,
    f_tuesday_time_beg_4           DATE,
    f_tuesday_time_end_1           DATE,
    f_tuesday_time_end_2           DATE,
    f_tuesday_time_end_3           DATE,
    f_tuesday_time_end_4           DATE,
    f_whensday_time_beg_1          DATE,
    f_whensday_time_beg_2          DATE,
    f_whensday_time_beg_3          DATE,
    f_whensday_time_beg_4          DATE,
    f_whensday_time_end_1          DATE,
    f_whensday_time_end_2          DATE,
    f_whensday_time_end_3          DATE,
    f_whensday_time_end_4          DATE,
    f_thursday_time_beg_1          DATE,
    f_thursday_time_beg_2          DATE,
    f_thursday_time_beg_3          DATE,
    f_thursday_time_beg_4          DATE,
    f_thursday_time_end_1          DATE,
    f_thursday_time_end_2          DATE,
    f_thursday_time_end_3          DATE,
    f_thursday_time_end_4          DATE,
    f_friday_time_beg_1            DATE,
    f_friday_time_beg_2            DATE,
    f_friday_time_beg_3            DATE,
    f_friday_time_beg_4            DATE,
    f_friday_time_end_1            DATE,
    f_friday_time_end_2            DATE,
    f_friday_time_end_3            DATE,
    f_friday_time_end_4            DATE,
    f_saturday_time_beg_1          DATE,
    f_saturday_time_beg_2          DATE,
    f_saturday_time_beg_3          DATE,
    f_saturday_time_beg_4          DATE,
    f_saturday_time_end_1          DATE,
    f_saturday_time_end_2          DATE,
    f_saturday_time_end_3          DATE,
    f_saturday_time_end_4          DATE,
    f_sunday_time_beg_1            DATE,
    f_sunday_time_beg_2            DATE,
    f_sunday_time_beg_3            DATE,
    f_sunday_time_beg_4            DATE,
    f_sunday_time_end_1            DATE,
    f_sunday_time_end_2            DATE,
    f_sunday_time_end_3            DATE,
    f_sunday_time_end_4            DATE,
    f_holiday_time_beg_1           DATE,
    f_holiday_time_beg_2           DATE,
    f_holiday_time_beg_3           DATE,
    f_holiday_time_beg_4           DATE,
    f_holiday_time_end_1           DATE,
    f_holiday_time_end_2           DATE,
    f_holiday_time_end_3           DATE,
    f_holiday_time_end_4           DATE,
    f_first_check                  nvarchar(1),
    f_fecond_check                 nvarchar(1),
    f_third_check                  nvarchar(1),
    f_last_check                   nvarchar(1))

ALTER TABLE vis_zones
ADD PRIMARY KEY (f_zone_id)

if not exists(select * from vis_zones where f_zone_id = '0')
begin
	insert into vis_zones values ('0', '', '', '', 'N', '', '', '', '', '','', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '')
end
go

-- Создание vis_zones_order_elements
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_zones_order_elements') is not null
	drop table vis_zones_order_elements;

CREATE TABLE vis_zones_order_elements
    (f_zone_order_element_id       int NOT NULL,
    f_oe_id                        int,
    f_zone_id                      int)

ALTER TABLE vis_zones_order_elements
ADD PRIMARY KEY (f_zone_order_element_id)

if not exists(select * from vis_zones_order_elements where f_zone_order_element_id = '0')
begin
	insert into vis_zones_order_elements values ( '0', '', '')
end
go

-- ===========================================

-- Создание БД VisitorsImages на T-SQL
-- ===========================================

if DB_ID('VisitorsImages') is not null
	drop database VisitorsImages;
create database VisitorsImages
COLLATE Cyrillic_General_CI_AS;
go

use VisitorsImages;
go

-- Создание vis_image
-- Таблица для хранения изображений произвольного назначения.

if OBJECT_ID('vis_image') is not null
	drop table vis_image;

CREATE TABLE vis_image
    (f_image_id                    int NOT NULL,
    f_image_alias                  UNIQUEIDENTIFIER NOT NULL,
    f_visitor_id                   int,
    f_image_type                   int,
    f_data                         VARBINARY(MAX),
    f_deleted                      CHAR(1))

ALTER TABLE vis_image
ADD PRIMARY KEY (f_image_id)

-- Создание vis_image_document.
-- Таблица привязки изображений к документам.

if OBJECT_ID('vis_image_document') is not null
	drop table vis_image_document;

CREATE TABLE vis_image_document
    (f_img_doc_id                  int NOT NULL,
    f_image_id                     int,
    f_doc_id                       int,
    f_deleted                      CHAR(1))

ALTER TABLE vis_image_document
ADD PRIMARY KEY (f_img_doc_id)

-- ===========================================

-- Создание БД VisitorsLogs на T-SQL
-- ===========================================

if DB_ID('VisitorsLogs') is not null
	drop database VisitorsLogs;

create database VisitorsLogs
COLLATE Cyrillic_General_CI_AS;
go

use VisitorsLogs;
go

-- Создание vis_log

if OBJECT_ID('vis_log') is not null
	drop table vis_log;

CREATE TABLE vis_log
    (f_log_id                      bigint NOT NULL,
	f_table_name                   nvarchar(1000),  -- если нужно, сюда можно писать название таблицы из базы Visitors для привязки
	f_table_id                     int,            -- если нужно, сюда можно писать id из таблицы базы Visitors для привязки
	f_rec_operator                 int,            -- если нужно, сюда можно писать id пользователя из таблицы базы Visitors для привязки
	f_log_severety                 nvarchar(50),    -- критичность события - может быть и числовым
	f_log_class                    nvarchar(MAX),   -- класс, в котором произошло событие
    f_log_message                  nvarchar(1000),
    f_rec_date                     DATE,
	f_comment                      nvarchar(200),   -- комментарий, на всякий случай
	f_machine                      nvarchar(200))   -- имя машины

ALTER TABLE vis_log
ADD PRIMARY KEY (f_log_id)

-- ===========================================