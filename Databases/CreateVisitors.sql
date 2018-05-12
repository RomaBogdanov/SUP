-- Создание БД Visitors на T-SQL
create database Visitors;
go

use Visitors;
go

-- Создание тестовой таблицы
create table TestTab
(
	C1 varchar(15),
	C2 varchar(15),
	C3 varchar(15)
)

-- Создание embarcadero_explain_plan

CREATE TABLE embarcadero_explain_plan
    (statement_id                   VARCHAR(30),
    timestamp                      DATE,
    remarks                        VARCHAR(80),
    operation                      VARCHAR(30),
    options                        VARCHAR(30),
    object_node                    VARCHAR(128),
    object_owner                   VARCHAR(30),
    object_name                    VARCHAR(30),
    object_instance                int,
    object_type                    VARCHAR(30),
    optimizer                      VARCHAR(255),
    search_columns                 int,
    id                             int,
    parent_id                      int,
    position                       int,
    cost                           int,
    cardinality                    int,
    bytes                          int,
    other_tag                      VARCHAR(255),
    partition_start                VARCHAR(255),
    partition_stop                 VARCHAR(255),
    partition_id                   int,
    other                          bigint)

-- Создание plan_table

CREATE TABLE plan_table
    (statement_id                   VARCHAR(30),
    timestamp                      DATE,
    remarks                        VARCHAR(80),
    operation                      VARCHAR(30),
    options                        VARCHAR(30),
    object_node                    VARCHAR(128),
    object_owner                   VARCHAR(30),
    object_name                    VARCHAR(30),
    object_instance                int,
    object_type                    VARCHAR(30),
    optimizer                      VARCHAR(255),
    search_columns                 int,
    id                             int,
    parent_id                      int,
    position                       int,
    cost                           int,
    cardinality                    int,
    bytes                          int,
    other_tag                      VARCHAR(255),
    partition_start                VARCHAR(255),
    partition_stop                 VARCHAR(255),
    partition_id                   int,
    other                          bigint,
    distribution                   VARCHAR(30))

-- Создание sqln_explain_plan

CREATE TABLE sqln_explain_plan
    (statement_id                   VARCHAR(30),
    timestamp                      DATE,
    remarks                        VARCHAR(80),
    operation                      VARCHAR(30),
    options                        VARCHAR(30),
    object_node                    VARCHAR(128),
    object_owner                   VARCHAR(30),
    object_name                    VARCHAR(30),
    object_instance                int,
    object_type                    VARCHAR(30),
    optimizer                      VARCHAR(255),
    search_columns                 int,
    id                             int,
    parent_id                      int,
    position                       int,
    cost                           int,
    cardinality                    int,
    bytes                          int,
    other_tag                      VARCHAR(255),
    partition_start                VARCHAR(255),
    partition_stop                 VARCHAR(255),
    partition_id                   int,
    other                          bigint,
    distribution                   VARCHAR(30))
 
-- Создание vip_order_items

CREATE TABLE vip_order_items
    (f_order_item_id               int NOT NULL,
    f_order_id                     int NOT NULL,
    f_visitor_id                   int NOT NULL,
    f_disabled                     VARCHAR(1),
    f_notes                        VARCHAR(200),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_catcher_id                   int,
    f_exists                       VARCHAR(1))

ALTER TABLE vip_order_items
ADD CONSTRAINT pk_vip_order_items PRIMARY KEY (f_order_item_id)

-- Создание vip_orders

CREATE TABLE vip_orders
    (f_order_id                    int NOT NULL,
    f_order_type                   VARCHAR(1),
    f_order_reg_num                int,
    f_catcher_id                   int,
    f_order_date                   DATE,
    f_date_from                    DATE,
    f_date_to                      DATE,
    f_time_from                    DATE,
    f_time_to                      DATE,
    f_disabled                     VARCHAR(1),
    f_notes                        VARCHAR(300),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vip_orders
ADD CONSTRAINT pk_vip_orders PRIMARY KEY (f_order_id)

-- Создание vip_visits

CREATE TABLE vip_visits
    (f_visit_id                    int NOT NULL,
    f_visitor_id                   int,
    f_catcher_id                   int,
    f_time_in                      DATE,
    f_time_out                     DATE,
    f_visit_text                   VARCHAR(400),
    f_date_from                    DATE,
    f_date_to                      DATE,
    f_order_id                     int,
    f_deleted                      VARCHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_create_date                  DATE,
    f_rec_operator_back            int,
    f_rec_date_back                DATE)

ALTER TABLE vip_visits
ADD CONSTRAINT pk_vip_visits PRIMARY KEY (f_visit_id)

-- Создание vis_acc

CREATE TABLE vis_acc
    (f_flag                         VARCHAR(1))

-- Создание vis_arh_orders
  
CREATE TABLE vis_arh_orders
    (f_ord_id                       int)

-- Создание vis_cabinets

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

-- Создание vis_cabinets_zones

CREATE TABLE vis_cabinets_zones
    (f_cabinet_id                  int,
    f_zone_id                      int,
    f_cabinet_zone_id              int NOT NULL)

ALTER TABLE vis_cabinets_zones
ADD CONSTRAINT pk_cabinets_zones PRIMARY KEY (f_cabinet_zone_id)

-- Создание vis_cabinets_zones_2

CREATE TABLE vis_cabinets_zones_2
    (f_cabinet_id                  int,
    f_zone_id                      int,
    f_cabinet_zone_id              int NOT NULL)

ALTER TABLE vis_cabinets_zones_2
ADD CONSTRAINT pk_cabinets_zones_2 PRIMARY KEY (f_cabinet_zone_id)

-- Создание vis_cards

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

-- Создание vis_countries

CREATE TABLE vis_countries
    (f_cntr_id                     int NOT NULL,
    f_cntr_name                    VARCHAR(50),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_countries
ADD PRIMARY KEY (f_cntr_id)

-- Создание vis_departaments

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

-- Создание vis_documents

CREATE TABLE vis_documents
    (f_doc_id                      int NOT NULL,
    f_doc_name                     VARCHAR(40),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_documents
ADD PRIMARY KEY (f_doc_id)

-- Создание vis_flag

CREATE TABLE vis_flag
    (f_user_id                     int,
    f_modified                     VARCHAR(1))
  
-- Создание vis_roles

CREATE TABLE vis_roles
    (f_role_id                     int,
    f_role                         VARCHAR(100),
    f_grb_id                       int)

-- Создание vis_role_lists

CREATE TABLE vis_role_lists
    (f_id                          int,
    f_role_id                      int,
    f_user_id                      int,
    f_status_id                    int)

-- Создание vis_users

CREATE TABLE vis_users
    (f_user_id                     int,
    f_user                         VARCHAR(100),
    f_pass                         VARCHAR(100),
	f_timeout                      int)

-- Создание vis_order_elements

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

-- Создание vis_orderlist

CREATE TABLE vis_orderlist
    (f_ord_id                      int,
    f_visit_id                     int,
    f_deleted                      CHAR(1),
    f_orderlist_id                 int NOT NULL)

ALTER TABLE vis_orderlist
ADD CONSTRAINT orderlist_idx_un PRIMARY KEY (f_orderlist_id)

-- Создание vis_orders

CREATE TABLE vis_orders
    (f_ord_id                      int NOT NULL,
    f_order_type_id                CHAR(18) NOT NULL,
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

insert into vis_orders (f_ord_id, f_order_type_id, f_signed_by, f_adjusted_with) values (0, 0, 0, 0)

-- Создание vis_organizations

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

insert into vis_organizations (f_org_id) values (0)

-- Создание vis_regions

CREATE TABLE vis_regions
    (f_region_id                   int NOT NULL,
    f_region_name                  VARCHAR(50),
    f_cntr_id                      int,
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_regions
ADD PRIMARY KEY (f_region_id)

-- Создание vis_spr_cardstates

CREATE TABLE vis_spr_cardstates
    (f_state_id                    int NOT NULL,
    f_state_text                   VARCHAR(50),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_spr_cardstates
ADD PRIMARY KEY (f_state_id)

-- Создание vis_spr_order_types

CREATE TABLE vis_spr_order_types
    (f_order_type_id               CHAR(18) NOT NULL,
    f_order_text                   VARCHAR(50),
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_spr_order_types
ADD PRIMARY KEY (f_order_type_id)

-- Создание vis_visitors

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

insert into vis_visitors (f_visitor_id, f_org_id) values (0, 0);
go

-- Создание vis_visitors_documents

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
	f_comment                      VARCHAR(100),
    f_deleted                      CHAR(1))

ALTER TABLE vis_visitors_documents
ADD PRIMARY KEY (f_vd_id)

-- Создание vis_visits

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

-- Создание vis_zone_types

CREATE TABLE vis_zone_types
    (f_zone_type_id                int NOT NULL,
    f_zone_type_name               VARCHAR(50) NOT NULL,
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_comment                      VARCHAR(200))

ALTER TABLE vis_zone_types
ADD PRIMARY KEY (f_zone_type_id)

-- Создание vis_zones

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

-- Создание vis_zones_order_elements

CREATE TABLE vis_zones_order_elements
    (f_zone_order_element_id       int NOT NULL,
    f_oe_id                        int,
    f_zone_id                      int)

ALTER TABLE vis_zones_order_elements
ADD PRIMARY KEY (f_zone_order_element_id)



