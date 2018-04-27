use Visitors;
go

-- Нулевые записи

if not exists(select * from vis_cabinets where f_cabinet_id = '0')
begin
	insert into vis_cabinets values ( '0', '', '', '', 'N', '', '', '')
end
go

if not exists(select * from vis_cabinets_zones where f_cabinet_zone_id = '0')
begin
	insert into vis_cabinets_zones values ( '', '', '0')
end
go

if not exists(select * from vis_cabinets_zones_2 where f_cabinet_zone_id = '0')
begin
	insert into vis_cabinets_zones_2 values ( '', '', '0')
end
go

if not exists(select * from vis_cards where f_card_id = '0')
begin
	insert into vis_cards values ( '0', '', '', '', 'N', '', '', '', '', '', '')
end
go

if not exists(select * from vis_countries where f_cntr_id = '0')
begin
	insert into vis_countries values ( '0', '', 'N', '', '')
end
go

if not exists(select * from vis_departament_sections where f_section_id = '0')
begin
	insert into vis_departament_sections values ( '0', '', '', '0', 'N', '', '')
end
go

if not exists(select * from vis_departaments where f_dep_id = '0')
begin
	insert into vis_departaments values ( '0', '', '', '', 'N', '', '')
end
go

if not exists(select * from vis_documents where f_doc_id = '0')
begin
	insert into vis_documents values ( '0', '', 'N', '', '')
end
go

if not exists(select * from vis_flag where f_user_id = '0')
begin
	insert into vis_flag values ( '0', 'N')
end
go

if not exists(select * from vis_new_roles where f_role_id = '0')
begin
	insert into vis_new_roles values ( '0', '', '')
end
go

if not exists(select * from vis_new_roles_list where f_id = '0')
begin
	insert into vis_new_roles_list values ( '0', '', '', '')
end
go

if not exists(select * from vis_new_user where f_user_id = '0')
begin
	insert into vis_new_user values ( '0', '', '', '')
end
go

if not exists(select * from vis_order_elements where f_oe_id = '0')
begin
	insert into vis_order_elements values ( '0', '', '', '', '', '', '', '', 'N', '', '', '', '', '')
end
go

if not exists(select * from vis_orderlist where f_orderlist_id = '0')
begin
	insert into vis_orderlist values ( '', '', 'N', '0')
end
go

if not exists(select * from vis_orders where f_ord_id = '0')
begin
	insert into vis_orders values ( '0', '0', '', '', '', '', '', '', '', '', 'N', '', '', '')
end
go

if not exists(select * from vis_organizations where f_org_id = '0')
begin
	insert into vis_organizations values ( '0', '', '', '', '', '', '', 'N', '', '', '', '', '')
end
go

if not exists(select * from vis_spr_cardstates where f_state_id = '0')
begin
	insert into vis_spr_cardstates values ( '0', '', 'N', '', '')
end
go

if not exists(select * from vis_spr_order_types where f_order_type_id = '0')
begin
	insert into vis_spr_order_types values ( '0', '', 'N', '', '')
end
go

if not exists(select * from vis_visitors where f_visitor_id = '0')
begin
	insert into vis_visitors values ( '0', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'N', '', '', '', '', '', '', '', '', '')
end
go

if not exists(select * from vis_visits where f_visit_id = '0')
begin
	insert into vis_visits values ( '0', '', '', '', '', '', '', '', '', 'N', '', '', '', '', '', '', '')
end
go

if not exists(select * from vis_zone_types where f_zone_type_id = '0')
begin
	insert into vis_zone_types values ( '0', '', 'N', '', '', '')
end
go

if not exists(select * from vis_zones where f_zone_id = '0')
begin
	insert into vis_zones values ('0', '', '', '', 'N', '', '', '', '', '','', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '')
end
go

if not exists(select * from vis_zones_order_elements where f_zone_order_element_id = '0')
begin
	insert into vis_zones_order_elements values ( '0', '', '')
end
go


-- Таймаут для пользователей
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

