use Visitors;
go


-- “аймаут дл€ пользователей
ALTER TABLE vis_new_user
ADD f_timeout int
go

UPDATE  vis_new_user SET f_timeout = 180
--UPDATE  vis_new_user SET f_timeout = -1 WHERE f_user_id=22
go

-- согласие на обработку персональных данных
ALTER TABLE vis_visitors
ADD
	f_personal_data_agreement CHAR(1),
	f_personal_data_last_date DATE
go

UPDATE vis_visitors SET f_personal_data_agreement='N', f_personal_data_last_date='' WHERE f_visitor_id>0
go

