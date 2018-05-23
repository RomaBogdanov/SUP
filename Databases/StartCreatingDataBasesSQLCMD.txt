-- Файл создания Баз данных с помощью параметров.
-- Visitors - параметр названия базы данных Visitors
-- VisitorsImages
-- VisitorsLogs
-- Пример использования sqlcmd:
-- sqlcmd -v Visitors="Visitors1" VisitorsImages="VisitorsImages1" VisitorsLogs="VisitorsLogs1" 
--		  -i E:\Reps\SUP develop\SUP\Databases\StartCreatingDataBasesSQLCMD.sql
-- Пакет названий для создания Баз Данных

use master;
go

-- Создание БД Visitors на T-SQL
-- ===========================================

if DB_ID('$(Visitors)') is not null
	drop database $(Visitors);
create database $(Visitors);
go

use $(Visitors);
go

-- Создание vis_cabinets
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_cabinets') is not null
	drop table vis_cabinets;

CREATE TABLE vis_cabinets
    (f_cabinet_id                  int NOT NULL,
    f_cabinet_num                  VARCHAR(50),
    f_cabinet_desc                 VARCHAR(200),
    f_door_num                     VARCHAR(20),
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
    f_card_text                    VARCHAR(200),
    f_last_visit_id                int,
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_create_date                  DATE,
    f_lost_date                    DATE,
    f_comment                      VARCHAR(200),
    f_card_num                     int)

ALTER TABLE vis_cards
ADD PRIMARY KEY (f_card_id)

if not exists(select * from vis_cards where f_card_id = '0')
begin
	insert into vis_cards values ( '0', '0', '', '0', 'N', '', '0', '', '', '', '0')
end
go

-- Создание vis_countries
-- Таблица списка стран. Пока используется для определения гражданств и стран из которых организации.

if OBJECT_ID('vis_countries') is not null
	drop table vis_countries;

CREATE TABLE vis_countries
    (f_cntr_id                     int NOT NULL,
    f_cntr_name                    VARCHAR(50),
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
    f_dep_name                     VARCHAR(100),
    f_short_dep_name               VARCHAR(15),
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
    f_doc_name                     VARCHAR(40),
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

-- Создание vis_flag
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_flag') is not null
	drop table vis_flag;

CREATE TABLE vis_flag
    (f_user_id                     int,
    f_modified                     VARCHAR(1))
	
if not exists(select * from vis_flag where f_user_id = '0')
begin
	insert into vis_flag values ( '0', 'N')
end
go

-- Создание vis_roles
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_roles') is not null
	drop table vis_roles;

CREATE TABLE vis_roles
    (f_role_id                     int,
    f_role                         VARCHAR(100),
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
    f_user                         VARCHAR(100),
    f_pass                         VARCHAR(100),
	f_timeout                      int)

if not exists(select * from vis_users where f_user_id = '0')
begin
	insert into vis_users values ( '0', '', '', '0')
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
    f_time_from                    DATE,
    f_time_to                      DATE,
    f_passes                       VARCHAR(1000),
    f_disabled                     VARCHAR(1),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_not_remaind                  VARCHAR(1),
    f_full_role                    VARCHAR(1),
    f_other_org                    VARCHAR(200))

ALTER TABLE vis_order_elements
ADD PRIMARY KEY (f_oe_id)

if not exists(select * from vis_order_elements where f_oe_id = '0')
begin
	insert into vis_order_elements values ( '0', '', '', '', '', '', '', '', 'N', '', '', '', '', '')
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
    f_notes                        VARCHAR(150),
    f_disabled                     VARCHAR(1),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_temp_posted                  VARCHAR(1))

ALTER TABLE vis_orders
ADD PRIMARY KEY (f_ord_id)

if not exists(select * from vis_orders where f_ord_id = '0')
begin
	insert into vis_orders values ( '0', '', '', '', '', '', '', '', '', '', 'N', '', '', '')
end
go

-- Создание vis_organizations
-- Важная таблица, список всех организаций используемых программой

if OBJECT_ID('vis_organizations') is not null
	drop table vis_organizations;

CREATE TABLE vis_organizations
    (f_org_id                      int NOT NULL,
    f_org_type                     VARCHAR(15),
    f_org_name                     VARCHAR(50),
    f_has_free_access              VARCHAR(1),
    f_is_master                    VARCHAR(1),
    f_is_basic                     VARCHAR(1),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_syn_id                       int,
    f_comment                      VARCHAR(200),
    f_full_org_name                VARCHAR(70),
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
    f_region_name                  VARCHAR(50),
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

-- Создание vis_spr_cardstates
-- Таблица статусов пропусков.

if OBJECT_ID('vis_spr_cardstates') is not null
	drop table vis_spr_cardstates

CREATE TABLE vis_spr_cardstates
    (f_state_id                    int NOT NULL,
    f_state_text                   VARCHAR(50),
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


-- Создание vis_spr_order_types
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_spr_order_types') is not null
	drop table vis_spr_order_types

CREATE TABLE vis_spr_order_types
    (f_order_type_id               int NOT NULL,
    f_order_text                   VARCHAR(50),
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

-- Создание vis_visitors
-- Одна из основных таблиц.
-- Таблица посетителей и сотрудников Транснефти.

if OBJECT_ID('vis_visitors') is not null
	drop table vis_visitors

CREATE TABLE vis_visitors
    (f_visitor_id                  int NOT NULL,
    f_doc_id                       int,
    f_cntr_id                      int,
    f_family                       VARCHAR(50),
    f_fst_name                     VARCHAR(20),
    f_sec_name                     VARCHAR(20),
    f_birth_date                   DATE,
    f_doc_seria                    VARCHAR(20),
    f_doc_num                      VARCHAR(20),
    f_doc_date                     DATE,
    f_doc_org                      VARCHAR(100),
    f_phones                       VARCHAR(50),
    f_vr_text                      VARCHAR(100),
    f_is_short_data                VARCHAR(1),
    f_dep_id                       int,
    f_job                          VARCHAR(50),
    f_can_sign_orders              VARCHAR(1),
    f_can_adjust_orders            VARCHAR(1),
    f_can_have_visitors            VARCHAR(1),
    f_persona_non_grata            VARCHAR(1),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_org_id                       int,
    f_rec_date_pass                DATE,
    f_rec_operator_pass            int,
    f_cabinet_id                   int,
    f_full_name                    VARCHAR(100),
    f_personal_data_agreement      CHAR(1),
    f_personal_data_last_date      DATE,
    f_doc_code                     VARCHAR(20))

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
    f_doc_name                     VARCHAR(100),
    f_doc_seria                    VARCHAR(20),
    f_doc_num                      VARCHAR(20),
    f_doc_date                     DATE,
	f_doc_date_to                  DATE,
    f_doc_org                      VARCHAR(100),
    f_doc_code                     VARCHAR(20),
	f_birth_date                   DATE,
	f_comment                      VARCHAR(100),
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
    f_card_id                      int,
    f_visitor_id                   int,
    f_time_out                     DATE,
    f_time_in                      DATE,
    f_visit_text                   VARCHAR(200),
    f_date_from                    DATE,
    f_date_to                      DATE,
    f_order_id                     int,
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_reason                       VARCHAR(1000),
    f_rec_operator_back            int,
    f_rec_date_back                DATE,
    f_card_status                  varchar(1),
    f_eff_zonen_text               VARCHAR(1000))

ALTER TABLE vis_visits
ADD PRIMARY KEY (f_visit_id)

if not exists(select * from vis_visits where f_visit_id = '0')
begin
	insert into vis_visits values ( '0', '', '', '', '', '', '', '', '', 'N', '', '', '', '', '', '', '')
end
go

-- Создание vis_zone_types
-- TODO: Сделать описание таблицы

if OBJECT_ID('vis_zone_types') is not null
	drop table vis_zone_types;

CREATE TABLE vis_zone_types
    (f_zone_type_id                int NOT NULL,
    f_zone_type_name               VARCHAR(50) NOT NULL,
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_comment                      VARCHAR(200))

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
    f_comment                      VARCHAR(200),
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
    f_first_check                  VARCHAR(1),
    f_fecond_check                 VARCHAR(1),
    f_third_check                  VARCHAR(1),
    f_last_check                   VARCHAR(1))

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

if DB_ID('$(VisitorsImages)') is not null
	drop database $(VisitorsImages);
create database $(VisitorsImages);
go

use $(VisitorsImages);
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

if DB_ID('$(VisitorsLogs)') is not null
	drop database $(VisitorsLogs);

create database $(VisitorsLogs);
go

use $(VisitorsLogs);
go

-- Создание vis_log

if OBJECT_ID('vis_log') is not null
	drop table vis_log;

CREATE TABLE vis_log
    (f_log_id                      bigint NOT NULL,
	f_table_name                   VARCHAR(1000),  -- если нужно, сюда можно писать название таблицы из базы Visitors для привязки
	f_table_id                     int,            -- если нужно, сюда можно писать id из таблицы базы Visitors для привязки
	f_rec_operator                 int,            -- если нужно, сюда можно писать id пользователя из таблицы базы Visitors для привязки
	f_log_severety                 VARCHAR(50),    -- критичность события - может быть и числовым
	f_log_class                    VARCHAR(MAX),   -- класс, в котором произошло событие
    f_log_message                  VARCHAR(1000),
    f_rec_date                     DATE,
	f_comment                      VARCHAR(200),   -- комментарий, на всякий случай
	f_machine                      VARCHAR(200))   -- имя машины

ALTER TABLE vis_log
ADD PRIMARY KEY (f_log_id)

-- ===========================================