﻿-- С 04.08.2018 ВСЕ ИЗМЕНЕНИЯ В БД ДОЛЖНЫ ЗАНОСИТЬСЯ В ЭТОТ СКРИПТ
-- ЧТОБЫ БЫЛА ВОЗМОЖНОСТЬ "НАКАТИТЬ" БАЗУ ДО АКТУАЛЬНОЙ, НЕ ПЕРЕСОЗДАВАЯ ЕЕ

-- Скрипт обновления Баз данных.


-- Создание vis_templates
-- Таблица списка шаблонов.

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
    f_deleted                      nvarchar(1),
    f_rec_date                     DATE,
    f_rec_operator                 int)

ALTER TABLE vis_templates
ADD PRIMARY KEY (f_template_id)

if not exists(select * from vis_templates where f_template_id = '0')
begin
	insert into vis_templates values ('0', '0', '', '', '', 'N', '', '0')
end
go


-- Удаление таблицы vis_areas_order_elements

use Visitors;
go

if OBJECT_ID('vis_areas_order_elements') is not null
	drop table vis_areas_order_elements;
go


-- Обновление таблицы vis_order_elements

use Visitors;
go

ALTER TABLE vis_order_elements
ADD
	f_oe_templates                 nvarchar(MAX),
	f_oe_areas                     nvarchar(MAX),
	f_schedule_id                  int
go

ALTER TABLE visitors.dbo.vis_order_elements ALTER COLUMN f_other_org nvarchar(1024);
go

UPDATE vis_order_elements SET f_oe_templates='', f_oe_areas='', f_schedule_id=0
go


-- Обновление таблицы vis_visits

use Visitors;
go

ALTER TABLE vis_visits
ADD
	f_orders                       nvarchar(MAX)
go

UPDATE vis_visits SET f_orders=''
go

-- Обновление таблицы vis_visitors
use Visitors;
go

ALTER TABLE vis_visitors
ADD
	f_no_formular nvarchar(1) DEFAULT 'N'
go

UPDATE vis_visitors SET f_no_formular='N'
go

-- Обновление таблицы vis_cards, Создание vis_cards_ext

use Visitors;
go

ALTER TABLE vis_cards
DROP COLUMN f_state_id, f_card_text, f_last_visit_id, f_deleted, f_rec_date, f_rec_operator, f_create_date, f_lost_date, f_comment
go

if OBJECT_ID('vis_cards_ext') is not null
	drop table vis_cards_ext;

create table vis_cards_ext
(
    f_card_id                      int not null,
    f_object_id_hi                 int,
    f_object_id_lo                 int,
	f_state_id                     int,
	f_card_text                    nvarchar(200),
    f_last_visit_id                int,
    f_deleted                      CHAR(1),
    f_rec_date                     DATE,
    f_rec_operator                 int,
    f_create_date                  DATE,
    f_lost_date                    DATE,
    f_comment                      nvarchar(200)
)

alter table vis_cards_ext
	add primary key (f_card_id)
go

if not exists(select * from vis_cards_ext where f_card_id = '0')
begin
	insert into vis_cards_ext values ( '0', '0', '0', '0', '', '0', 'N', '', '0', '', '', '')
end
go