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

insert into vis_cabinets values ( '0', '', '', '', 'N', '', '', '')
go

insert into vis_cabinets_zones values ( '', '', '0')
go

insert into vis_cabinets_zones_2 values ( '', '', '0')
go

insert into vis_cards values ( '0', '', '', '', 'N', '', '', '', '', '', '')
go

insert into vis_countries values ( '0', '', 'N', '', '')
go

insert into vis_departament_sections values ( '0', '', '', '0', 'N', '', '')
go

insert into vis_departaments values ( '0', '', '', '', 'N', '', '')
go

insert into vis_documents values ( '0', '', 'N', '', '')
go

insert into vis_flag values ( '0', 'N')
go

insert into vis_new_roles values ( '0', '', '')
go

insert into vis_new_roles_list values ( '0', '', '', '')
go

insert into vis_new_user values ( '0', '', '', '')
go

insert into vis_order_elements values ( '0', '', '', '', '', '', '', '', 'N', '', '', '', '', '')
go

insert into vis_orderlist values ( '', '', 'N', '0')
go

insert into vis_orders values ( '0', '0', '', '', '', '', '', '', '', '', 'N', '', '', '')
go

insert into vis_organizations values ( '0', '', '', '', '', '', '', 'N', '', '', '', '', '', '', '')
go

insert into vis_regions values ( '0', '', 'N', '', '')
go

insert into vis_spr_cardstates values ( '0', '', 'N', '', '')
go

insert into vis_spr_order_types values ( '0', '', 'N', '', '')
go

insert into vis_visitors values ( '0', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'N', '', '', '', '', '', '', '', '', '')
go

insert into vis_visits values ( '0', '', '', '', '', '', '', '', '', 'N', '', '', '', '', '', '', '')
go

insert into vis_zone_types values ( '0', '', 'N', '', '', '')
go

insert into vis_zones values ('0', '', '', '', 'N', '', '', '', '', '','', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '')
go

insert into vis_zones_order_elements values ( '0', '', '')
go
