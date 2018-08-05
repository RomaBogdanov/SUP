-- С 04.08.2018 ВСЕ ИЗМЕНЕНИЯ В БД ДОЛЖНЫ ЗАНОСИТЬСЯ В ЭТОТ СКРИПТ
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
