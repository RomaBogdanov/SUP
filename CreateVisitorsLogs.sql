-- Создание БД VisitorsLogs на T-SQL
create database VisitorsLogs;
go

use VisitorsLogs;
go

-- Создание vis_log
CREATE TABLE vis_log
    (f_log_id                      bigint NOT NULL IDENTITY(1,1),
	f_table_name                   VARCHAR(1000),  -- если нужно, сюда можно писать название таблицы из базы Visitors для привязки
	f_table_id                     int,            -- если нужно, сюда можно писать id из таблицы базы Visitors для привязки
	f_rec_operator                 int,            -- если нужно, сюда можно писать id пользователя из таблицы базы Visitors для привязки
	f_log_severety                 VARCHAR(50),    -- критичность события - может быть и числовым
	f_log_class                    VARCHAR(MAX),   -- класс, в котором произошло событие
    f_log_message                  VARCHAR(1000),
    f_rec_date                     DATE,
	f_comment                      VARCHAR(200))   -- комментарий, на всякий случай

ALTER TABLE vis_log
ADD PRIMARY KEY (f_log_id)
