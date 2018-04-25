use Visitors;
go
/*
delete vis_orders where f_ord_id='0' 
go

insert into vis_orders (f_ord_id, f_order_type_id) values ('0', '0')
go*/

-- Добавление начальных данных в таблицу vis_orders
insert into vis_orders (
f_ord_id, f_order_type_id, f_reg_number, 
f_ord_date, f_date_from, f_date_to, f_signed_by, 
f_adjusted_with, f_notes, f_disabled, f_deleted,
f_rec_date, f_rec_operator, f_temp_posted) 
values ('0', '0', '0', GETDATE(), GETDATE(), GETDATE(), 
'0', '0', '', 'N', 'N', GETDATE(), '0', 'N')
go

select * from vis_orders;
go

insert into vis_visitors values ( '0', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '')
go
