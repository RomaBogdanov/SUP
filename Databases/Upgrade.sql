use Visitors;
go


-- Таймаут для пользователей
ALTER TABLE vis_new_user
ADD f_timeout int

UPDATE  vis_new_user SET f_timeout = 180
--UPDATE  vis_new_user SET f_timeout = -1 WHERE f_user_id=22

