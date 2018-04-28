use Visitors;
go
-- ���������� ������ ��������� �������

-- ���������� �������� ������� �������
delete TestTab
insert into TestTab values ('Hello', 'World', '!');
go
insert into TestTab values ('Bia', 'is', 'Hia');
go

-- ���������� ������� vis_acc
delete vis_acc
insert into vis_acc values ( 'N')
insert into vis_acc values ( 'N')

-- ���������� ������� vis_cabinets
delete vis_cabinets
insert into vis_cabinets values ( '1', '105', '���. �����', '1', 'N', '29-���-2017 11:18:21', '22', '1')
insert into vis_cabinets values ( '2', '106', '����. ���������', '2', 'N', '29-���-2017 11:18:55', '22', '2')
insert into vis_cabinets values ( '3', '107', '�����������', '3', 'N', '29-���-2017 11:19:18', '22', '3')
insert into vis_cabinets values ( '21', '107', '�����������', '4', 'N', '29-���-2017 11:19:35', '22', '4')
insert into vis_cabinets values ( '22', '113', '����� ������', '6', 'N', '29-���-2017 11:21:05', '22', '6')
insert into vis_cabinets values ( '23', '-', '������� 1-� ����', '8', 'N', '29-���-2017 11:26:26', '22', '8')
insert into vis_cabinets values ( '41', '110', '��. �����', '5', 'N', '29-���-2017 11:20:31', '22', '5')
insert into vis_cabinets values ( '42', '-', '������� ����', '9', 'N', '29-���-2017 11:26:14', '22', '9')
insert into vis_cabinets values ( '43', '-', '������� 2-�� �����', '10', 'N', '29-���-2017 11:27:47', '22', '10')
insert into vis_cabinets values ( '44', '209', '����������������', '11', 'N', '29-���-2017 11:28:12', '22', '11')
insert into vis_cabinets values ( '45', '203', '��������', '12', 'N', '29-���-2017 11:28:27', '22', '12')
insert into vis_cabinets values ( '46', '208', '���', '13', 'N', '29-���-2017 11:28:42', '22', '13')
insert into vis_cabinets values ( '47', '112', '������', '7', 'N', '29-���-2017 11:29:00', '22', '7')
insert into vis_cabinets values ( '48', '206', '��', '14', 'N', '29-���-2017 11:29:24', '22', '14')
insert into vis_cabinets values ( '49', '207', '��', '15', 'N', '29-���-2017 11:29:39', '22', '15')

-- ���������� ������� vis_cabinets_zones
delete vis_cabinets_zones;
insert into vis_cabinets_zones values ( '42', '1', '80')
insert into vis_cabinets_zones values ( '21', '22', '124')
insert into vis_cabinets_zones values ( '23', '21', '116')
insert into vis_cabinets_zones values ( '2', '41', '93')
insert into vis_cabinets_zones values ( '43', '42', '96')
insert into vis_cabinets_zones values ( '45', '42', '97')
insert into vis_cabinets_zones values ( '23', '22', '125')
insert into vis_cabinets_zones values ( '42', '22', '126')
insert into vis_cabinets_zones values ( '42', '21', '117')
insert into vis_cabinets_zones values ( '23', '42', '94')
insert into vis_cabinets_zones values ( '42', '42', '95')

-- ���������� ������� vis_cabinets_zones_2
delete vis_cabinets_zones_2
insert into vis_cabinets_zones_2 values ( '3', '42', '99')
insert into vis_cabinets_zones_2 values ( '43', '1', '82')
insert into vis_cabinets_zones_2 values ( '21', '21', '120')
insert into vis_cabinets_zones_2 values ( '21', '42', '100')
insert into vis_cabinets_zones_2 values ( '41', '42', '101')
insert into vis_cabinets_zones_2 values ( '41', '21', '121')
insert into vis_cabinets_zones_2 values ( '22', '21', '122')
insert into vis_cabinets_zones_2 values ( '47', '21', '123')
insert into vis_cabinets_zones_2 values ( '1', '21', '118')
insert into vis_cabinets_zones_2 values ( '23', '1', '81')
insert into vis_cabinets_zones_2 values ( '3', '21', '119')
insert into vis_cabinets_zones_2 values ( '22', '42', '102')
insert into vis_cabinets_zones_2 values ( '47', '42', '103')
insert into vis_cabinets_zones_2 values ( '44', '42', '104')
insert into vis_cabinets_zones_2 values ( '46', '42', '105')
insert into vis_cabinets_zones_2 values ( '48', '42', '106')
insert into vis_cabinets_zones_2 values ( '49', '42', '107')
insert into vis_cabinets_zones_2 values ( '2', '22', '127')
insert into vis_cabinets_zones_2 values ( '1', '42', '98')

-- ���������� ������� vis_cards
delete vis_cards
insert into vis_cards values ( '1', '3', '1', '', 'N', '29-���-2017 12:54:01', '22', '20-���-2017 11:12:13', '', '1', '1')
insert into vis_cards values ( '2', '1', '2', '', 'N', '20-���-2017 11:12:31', '2', '20-���-2017 11:12:26', '', '2', '2')
insert into vis_cards values ( '3', '1', '3', '', 'N', '29-���-2017 12:51:50', '22', '20-���-2017 11:12:33', '', '3', '3')

-- ���������� ������� vis_countries
delete vis_countries
insert into vis_countries values ( '1', '������', 'N', '19-���-2017 16:04:22', '2')
insert into vis_countries values ( '21', '�������', 'N', '20-���-2017 10:58:45', '2')
insert into vis_countries values ( '22', '��������', 'N', '20-���-2017 10:59:08', '2')
insert into vis_countries values ( '41', '����� ����� ������', 'N', '25-���-2017 17:50:30', '2')

-- ���������� ������� vis_departament_sections
delete vis_departament_sections
insert into vis_departament_sections values ( '1', '����� ��������������', '', '1', 'N', '22-���-2017 12:54:10', '2')

-- ���������� ������� vis_departaments
delete vis_departaments
insert into vis_departaments values ( '1', '1', '�����������', '', 'N', '22-���-2017 12:53:32', '2')
insert into vis_departaments values ( '21', '1', '�����������', '', 'N', '29-���-2017 11:53:09', '22')
insert into vis_departaments values ( '22', '1', '����� ������', '', 'N', '29-���-2017 11:53:21', '22')
insert into vis_departaments values ( '23', '1', '����������� ����������', '', 'N', '29-���-2017 11:53:40', '22')

-- ���������� ������� vis_documents
delete vis_documents
insert into vis_documents values ( '1', '�������', 'N', '19-���-2017 16:04:39', '2')
insert into vis_documents values ( '21', '������������� ������', 'N', '25-���-2017 17:49:30', '2')

-- ���������� ������� vis_flag
delete vis_flag
insert into vis_flag values ( '1', 'Y')
insert into vis_flag values ( '2', 'Y')
insert into vis_flag values ( '22', 'Y')

-- ���������� ������� vis_new_roles
delete vis_new_roles
insert into vis_new_roles values ( '1', '�����������������', '1')
insert into vis_new_roles values ( '2', '����������', '2')
insert into vis_new_roles values ( '3', '���������', '3')
insert into vis_new_roles values ( '4', '������', '4')
insert into vis_new_roles values ( '5', '�����������', '5')
insert into vis_new_roles values ( '6', '�������� ����������� ��������', '5')
insert into vis_new_roles values ( '7', '������� �����������', '5')
insert into vis_new_roles values ( '8', '��������� ������� �����������', '5')
insert into vis_new_roles values ( '9', '�����������', '5')
insert into vis_new_roles values ( '10', '���������', '5')
insert into vis_new_roles values ( '11', '���������� (����)', '5')
insert into vis_new_roles values ( '12', '��������', '5')
insert into vis_new_roles values ( '13', '��������', '5')
insert into vis_new_roles values ( '14', '���� �������', '5')
insert into vis_new_roles values ( '15', '��������', '6')
insert into vis_new_roles values ( '16', '���������', '6')
insert into vis_new_roles values ( '17', '�����������', '7')
insert into vis_new_roles values ( '18', '����� ������', '8')
insert into vis_new_roles values ( '19', '����� "������ �����������"', '9')
insert into vis_new_roles values ( '20', '����� "� ������ ���������"', '9')
insert into vis_new_roles values ( '21', '����� "�� ��������� ���������"', '9')
insert into vis_new_roles values ( '22', '����� "� ��������� ���������"', '9')
insert into vis_new_roles values ( '23', '����� "�� �������"', '9')
insert into vis_new_roles values ( '24', '��������� ����� � ��������� ��������', '9')
insert into vis_new_roles values ( '25', '����� � ������� �������� �������', '9')
insert into vis_new_roles values ( '26', '����������', '10')
insert into vis_new_roles values ( '27', '���� ���������', '10')
insert into vis_new_roles values ( '28', '�������� �� ������', '10')
insert into vis_new_roles values ( '29', '��������', '10')
insert into vis_new_roles values ( '30', '������ �� ����������� ��� ���������', '11')
insert into vis_new_roles values ( '31', '��������� ����������� ��� ��������� - ������� ����', '12')
insert into vis_new_roles values ( '32', '��������� ����������� ��� ��������� - ��� ���������', '13')
insert into vis_new_roles values ( '33', '����� �� ������ ��� ������', '9')

-- ���������� ������� vis_new_roles_list
delete vis_new_roles_list
insert into vis_new_roles_list values ( '1', '1', '1', '2')
insert into vis_new_roles_list values ( '2', '2', '1', '0')
insert into vis_new_roles_list values ( '3', '3', '1', '0')
insert into vis_new_roles_list values ( '4', '4', '1', '0')
insert into vis_new_roles_list values ( '5', '5', '1', '0')
insert into vis_new_roles_list values ( '6', '6', '1', '0')
insert into vis_new_roles_list values ( '7', '7', '1', '0')
insert into vis_new_roles_list values ( '8', '8', '1', '0')
insert into vis_new_roles_list values ( '9', '9', '1', '0')
insert into vis_new_roles_list values ( '10', '10', '1', '0')
insert into vis_new_roles_list values ( '11', '11', '1', '0')
insert into vis_new_roles_list values ( '12', '12', '1', '0')
insert into vis_new_roles_list values ( '13', '13', '1', '0')
insert into vis_new_roles_list values ( '14', '14', '1', '0')
insert into vis_new_roles_list values ( '15', '15', '1', '0')
insert into vis_new_roles_list values ( '16', '16', '1', '0')
insert into vis_new_roles_list values ( '17', '17', '1', '0')
insert into vis_new_roles_list values ( '18', '18', '1', '0')
insert into vis_new_roles_list values ( '19', '19', '1', '0')
insert into vis_new_roles_list values ( '20', '20', '1', '0')
insert into vis_new_roles_list values ( '21', '21', '1', '0')
insert into vis_new_roles_list values ( '22', '22', '1', '0')
insert into vis_new_roles_list values ( '23', '23', '1', '0')
insert into vis_new_roles_list values ( '24', '24', '1', '0')
insert into vis_new_roles_list values ( '25', '25', '1', '0')
insert into vis_new_roles_list values ( '26', '26', '1', '0')
insert into vis_new_roles_list values ( '27', '27', '1', '0')
insert into vis_new_roles_list values ( '28', '28', '1', '0')
insert into vis_new_roles_list values ( '29', '29', '1', '0')
insert into vis_new_roles_list values ( '30', '30', '1', '0')
insert into vis_new_roles_list values ( '31', '31', '1', '0')
insert into vis_new_roles_list values ( '32', '32', '1', '0')
insert into vis_new_roles_list values ( '33', '33', '1', '0')
insert into vis_new_roles_list values ( '34', '1', '2', '2')
insert into vis_new_roles_list values ( '35', '2', '2', '2')
insert into vis_new_roles_list values ( '36', '3', '2', '2')
insert into vis_new_roles_list values ( '37', '4', '2', '2')
insert into vis_new_roles_list values ( '38', '5', '2', '2')
insert into vis_new_roles_list values ( '39', '6', '2', '2')
insert into vis_new_roles_list values ( '40', '7', '2', '2')
insert into vis_new_roles_list values ( '41', '8', '2', '2')
insert into vis_new_roles_list values ( '42', '9', '2', '2')
insert into vis_new_roles_list values ( '43', '10', '2', '2')
insert into vis_new_roles_list values ( '44', '11', '2', '2')
insert into vis_new_roles_list values ( '45', '12', '2', '2')
insert into vis_new_roles_list values ( '46', '13', '2', '2')
insert into vis_new_roles_list values ( '47', '14', '2', '2')
insert into vis_new_roles_list values ( '48', '15', '2', '2')
insert into vis_new_roles_list values ( '49', '16', '2', '2')
insert into vis_new_roles_list values ( '50', '17', '2', '2')
insert into vis_new_roles_list values ( '51', '18', '2', '2')
insert into vis_new_roles_list values ( '52', '19', '2', '1')
insert into vis_new_roles_list values ( '53', '20', '2', '1')
insert into vis_new_roles_list values ( '54', '21', '2', '1')
insert into vis_new_roles_list values ( '55', '22', '2', '1')
insert into vis_new_roles_list values ( '56', '23', '2', '1')
insert into vis_new_roles_list values ( '57', '24', '2', '1')
insert into vis_new_roles_list values ( '58', '25', '2', '1')
insert into vis_new_roles_list values ( '59', '26', '2', '1')
insert into vis_new_roles_list values ( '60', '27', '2', '0')
insert into vis_new_roles_list values ( '61', '28', '2', '0')
insert into vis_new_roles_list values ( '62', '29', '2', '0')
insert into vis_new_roles_list values ( '63', '30', '2', '2')
insert into vis_new_roles_list values ( '64', '31', '2', '2')
insert into vis_new_roles_list values ( '65', '32', '2', '2')
insert into vis_new_roles_list values ( '66', '33', '2', '1')
insert into vis_new_roles_list values ( '1', '1', '11', '2')
insert into vis_new_roles_list values ( '2', '2', '11', '0')
insert into vis_new_roles_list values ( '3', '3', '11', '0')
insert into vis_new_roles_list values ( '4', '4', '11', '0')
insert into vis_new_roles_list values ( '5', '5', '11', '0')
insert into vis_new_roles_list values ( '6', '6', '11', '0')
insert into vis_new_roles_list values ( '7', '7', '11', '0')
insert into vis_new_roles_list values ( '8', '8', '11', '0')
insert into vis_new_roles_list values ( '9', '9', '11', '0')
insert into vis_new_roles_list values ( '10', '10', '11', '0')
insert into vis_new_roles_list values ( '11', '11', '11', '0')
insert into vis_new_roles_list values ( '12', '12', '11', '0')
insert into vis_new_roles_list values ( '13', '13', '11', '0')
insert into vis_new_roles_list values ( '14', '14', '11', '0')
insert into vis_new_roles_list values ( '15', '15', '11', '0')
insert into vis_new_roles_list values ( '16', '16', '11', '0')
insert into vis_new_roles_list values ( '17', '17', '11', '0')
insert into vis_new_roles_list values ( '18', '18', '11', '0')
insert into vis_new_roles_list values ( '19', '19', '11', '0')
insert into vis_new_roles_list values ( '20', '20', '11', '0')
insert into vis_new_roles_list values ( '21', '21', '11', '0')
insert into vis_new_roles_list values ( '22', '22', '11', '0')
insert into vis_new_roles_list values ( '23', '23', '11', '0')
insert into vis_new_roles_list values ( '24', '24', '11', '0')
insert into vis_new_roles_list values ( '25', '25', '11', '0')
insert into vis_new_roles_list values ( '26', '26', '11', '0')
insert into vis_new_roles_list values ( '27', '27', '11', '0')
insert into vis_new_roles_list values ( '28', '28', '11', '0')
insert into vis_new_roles_list values ( '29', '29', '11', '0')
insert into vis_new_roles_list values ( '30', '30', '11', '0')
insert into vis_new_roles_list values ( '31', '31', '11', '0')
insert into vis_new_roles_list values ( '32', '32', '11', '0')
insert into vis_new_roles_list values ( '33', '33', '11', '0')
insert into vis_new_roles_list values ( '34', '1', '12', '2')
insert into vis_new_roles_list values ( '35', '2', '12', '2')
insert into vis_new_roles_list values ( '36', '3', '12', '2')
insert into vis_new_roles_list values ( '37', '4', '12', '2')
insert into vis_new_roles_list values ( '38', '5', '12', '2')
insert into vis_new_roles_list values ( '39', '6', '12', '2')
insert into vis_new_roles_list values ( '40', '7', '12', '2')
insert into vis_new_roles_list values ( '41', '8', '12', '2')
insert into vis_new_roles_list values ( '42', '9', '12', '2')
insert into vis_new_roles_list values ( '43', '10', '12', '2')
insert into vis_new_roles_list values ( '44', '11', '12', '2')
insert into vis_new_roles_list values ( '45', '12', '12', '2')
insert into vis_new_roles_list values ( '46', '13', '12', '2')
insert into vis_new_roles_list values ( '47', '14', '12', '2')
insert into vis_new_roles_list values ( '48', '15', '12', '2')
insert into vis_new_roles_list values ( '49', '16', '12', '2')
insert into vis_new_roles_list values ( '50', '17', '12', '2')
insert into vis_new_roles_list values ( '51', '18', '12', '2')
insert into vis_new_roles_list values ( '52', '19', '12', '1')
insert into vis_new_roles_list values ( '53', '20', '12', '1')
insert into vis_new_roles_list values ( '54', '21', '12', '1')
insert into vis_new_roles_list values ( '55', '22', '12', '1')
insert into vis_new_roles_list values ( '56', '23', '12', '1')
insert into vis_new_roles_list values ( '57', '24', '12', '1')
insert into vis_new_roles_list values ( '58', '25', '12', '1')
insert into vis_new_roles_list values ( '59', '26', '12', '1')
insert into vis_new_roles_list values ( '60', '27', '12', '0')
insert into vis_new_roles_list values ( '61', '28', '12', '0')
insert into vis_new_roles_list values ( '62', '29', '12', '0')
insert into vis_new_roles_list values ( '63', '30', '12', '2')
insert into vis_new_roles_list values ( '64', '31', '12', '2')
insert into vis_new_roles_list values ( '65', '32', '12', '2')
insert into vis_new_roles_list values ( '66', '33', '12', '1')
insert into vis_new_roles_list values ( '74', '1', '22', '0')
insert into vis_new_roles_list values ( '75', '2', '22', '2')
insert into vis_new_roles_list values ( '76', '3', '22', '2')
insert into vis_new_roles_list values ( '77', '4', '22', '2')
insert into vis_new_roles_list values ( '78', '5', '22', '2')
insert into vis_new_roles_list values ( '79', '6', '22', '2')
insert into vis_new_roles_list values ( '80', '7', '22', '2')
insert into vis_new_roles_list values ( '81', '8', '22', '2')
insert into vis_new_roles_list values ( '82', '9', '22', '2')
insert into vis_new_roles_list values ( '83', '10', '22', '2')
insert into vis_new_roles_list values ( '84', '11', '22', '2')
insert into vis_new_roles_list values ( '85', '12', '22', '2')
insert into vis_new_roles_list values ( '86', '13', '22', '2')
insert into vis_new_roles_list values ( '87', '14', '22', '2')
insert into vis_new_roles_list values ( '88', '15', '22', '0')
insert into vis_new_roles_list values ( '89', '16', '22', '0')
insert into vis_new_roles_list values ( '90', '17', '22', '1')
insert into vis_new_roles_list values ( '91', '18', '22', '1')
insert into vis_new_roles_list values ( '92', '19', '22', '1')
insert into vis_new_roles_list values ( '93', '20', '22', '1')
insert into vis_new_roles_list values ( '94', '21', '22', '1')
insert into vis_new_roles_list values ( '95', '22', '22', '1')
insert into vis_new_roles_list values ( '96', '23', '22', '1')
insert into vis_new_roles_list values ( '97', '24', '22', '1')
insert into vis_new_roles_list values ( '98', '25', '22', '1')
insert into vis_new_roles_list values ( '99', '26', '22', '0')
insert into vis_new_roles_list values ( '100', '27', '22', '1')
insert into vis_new_roles_list values ( '101', '28', '22', '0')
insert into vis_new_roles_list values ( '102', '29', '22', '0')
insert into vis_new_roles_list values ( '103', '30', '22', '0')
insert into vis_new_roles_list values ( '104', '31', '22', '0')
insert into vis_new_roles_list values ( '105', '32', '22', '0')
insert into vis_new_roles_list values ( '106', '33', '22', '1')

-- ���������� ������� vis_new_user
delete vis_new_user
insert into vis_new_user values ( '1', 'test', 'test', '180')
insert into vis_new_user values ( '2', 'VISITORS', 'visitorpsw', '180')
insert into vis_new_user values ( '11', '1', '1', '180')
insert into vis_new_user values ( '12', '2', '2', '180')
insert into vis_new_user values ( '22', '3', '3', '180')

-- ���������� ������� vis_order_elements
delete vis_order_elements where f_oe_id <> '0'
insert into vis_order_elements values ( '62', '63', '22', '', '', '', '5', 'N', 'N', '29-���-2017 12:08:03', '22', 'N', 'N', '')
insert into vis_order_elements values ( '3', '2', '2', '', '', '', '1', 'Y', 'N', '29-���-2017 12:51:50', '22', 'N', 'N', '')
insert into vis_order_elements values ( '41', '41', '2', '', '', '', '3', 'Y', 'N', '29-���-2017 12:51:50', '22', 'N', 'N', '')
insert into vis_order_elements values ( '2', '1', '2', '0', '30-���-1899', '30-���-1899', '1', 'Y', 'N', '29-���-2017 11:45:12', '22', 'N', 'N', '')
insert into vis_order_elements values ( '61', '61', '21', '2', '30-���-1899 9:00:00', '30-���-1899 18:00:00', '1', 'N', 'N', '29-���-2017 12:54:01', '22', 'Y', 'N', '')
insert into vis_order_elements values ( '67', '68', '21', '22', '30-���-1899 13:00:00', '30-���-1899 18:00:00', '2', 'N', 'N', '29-���-2017 12:56:44', '22', 'N', 'N', '')
insert into vis_order_elements values ( '66', '67', '23', '0', '30-���-1899', '30-���-1899', '5', 'N', 'N', '29-���-2017 12:24:47', '22', 'N', 'N', '')
insert into vis_order_elements values ( '65', '66', '21', '2', '30-���-1899 9:00:00', '30-���-1899 18:00:00', '1, 3', 'N', 'N', '29-���-2017 13:16:32', '22', 'N', 'N', '')

-- ���������� ������� vis_orderlist
delete vis_orderlist
insert into vis_orderlist values ( '2', '1', 'Y', '1')
insert into vis_orderlist values ( '2', '21', 'Y', '21')
insert into vis_orderlist values ( '41', '21', 'Y', '22')
insert into vis_orderlist values ( '61', '41', 'N', '41')
insert into vis_orderlist values ( '66', '41', 'N', '42')

-- ���������� ������� vis_orders
delete vis_orders where f_ord_id <> '0'
insert into vis_orders values ( '1', '2', '4', '20-���-2017 11:03:37', '20-���-2017', '30-���-2017', '2', '2', '', 'N', 'N', '29-���-2017 10:46:46', '22', 'N')
insert into vis_orders values ( '2', '4', '1', '20-���-2017 11:13:20', '20-���-2017', '30-���-2017', '', '', '�����������', 'N', 'N', '20-���-2017 11:13:20', '2', 'N')
insert into vis_orders values ( '21', '2', '2', '22-���-2017 12:50:15', '6-���-2017', '29-���-2017', '2', '', '', 'N', 'N', '22-���-2017 12:52:40', '2', 'N')
insert into vis_orders values ( '41', '4', '3', '13-���-2017 0:22:06', '', '', '', '', '�������', 'N', 'N', '13-���-2017 0:22:06', '2', 'N')
insert into vis_orders values ( '61', '1', '5', '29-���-2017 10:57:35', '29-���-2017', '29-���-2017', '2', '', '', 'N', 'N', '29-���-2017 10:59:23', '22', 'N')
insert into vis_orders values ( '63', '4', '6', '29-���-2017 12:08:03', '29-���-2017', '31-���-2025', '', '', '��������� ��������', 'N', 'N', '29-���-2017 12:08:03', '22', 'N')
insert into vis_orders values ( '66', '1', '7', '29-���-2017 12:09:30', '29-���-2017', '29-���-2017', '2', '', '', 'N', 'N', '29-���-2017 13:16:32', '22', 'N')
insert into vis_orders values ( '67', '2', '8', '29-���-2017 12:19:14', '29-���-2017', '31-���-2017', '2', '22', '', 'N', 'N', '29-���-2017 12:24:46', '22', 'N')
insert into vis_orders values ( '68', '1', '9', '29-���-2017 12:56:21', '29-���-2017', '29-���-2017', '22', '', '', 'N', 'N', '29-���-2017 12:56:44', '22', 'N')

-- ���������� �������  vis_organizations
delete vis_organizations where f_org_id <> '0'
insert into vis_organizations values ( '1', '', '���', '"�����"', 'N', 'N', 'Y', 'N', '29-���-2017 11:50:55', '22', '', '�������� �����������', '��� "�����"', '', '')
insert into vis_organizations values ( '2', '', '���', '"������������ �������� ���"', 'N', 'N', 'N', 'N', '29-���-2017 11:42:05', '22', '1', '', '��� "������������ �������� ���"', '', '')
insert into vis_organizations values ( '21', '', '��� ������', '" "', 'N', 'N', 'N', 'N', '14-���-2017 16:47:44', '2', '', '', '��� ������ " "', '', '')
insert into vis_organizations values ( '41', '', '���', '"�������"', 'N', 'N', 'N', 'N', '29-���-2017 10:53:05', '22', '', '', '��� "�������"', '', '')
insert into vis_organizations values ( '42', '', '���', '"����������� ������� ������"', 'Y', 'N', 'N', 'N', '29-���-2017 13:20:14', '22', '', '', '��� "����������� ������� ������"', '', '')
insert into vis_organizations values ( '43', '', '���', '"���"', 'N', 'N', 'N', 'N', '29-���-2017 11:40:00', '22', '42', '', '��� "���"', '', '')
insert into vis_organizations values ( '44', '', '���', '"�����"', 'N', 'N', 'N', 'N', '29-���-2017 11:43:48', '22', '42', '', '��� "�����"', '', '')

-- ���������� ������� vis_spr_cardstates
delete vis_spr_cardstates
insert into vis_spr_cardstates values ( '1', '�������', 'N', '11-���-2003 14:41:24', '-1')
insert into vis_spr_cardstates values ( '3', '�����', 'N', '11-���-2003 14:41:25', '-1')
insert into vis_spr_cardstates values ( '4', '������', 'N', '11-���-2003 14:41:28', '-1')
insert into vis_spr_cardstates values ( '2', '���������', 'N', '11-���-2003 14:41:58', '-1')

-- ���������� ������� vis_spr_order_types
delete vis_spr_order_types
insert into vis_spr_order_types values ( '1', '�������', 'N', '11-���-2003 14:39:56', '-1')
insert into vis_spr_order_types values ( '2', '���������', 'N', '11-���-2003 14:39:56', '-1')
insert into vis_spr_order_types values ( '3', '����������', 'N', '11-���-2003 14:39:56', '-1')
insert into vis_spr_order_types values ( '4', '�� ���������', 'N', '11-���-2003 14:39:57', '-1')

-- ������ ������� ������������� - �� ���������
--insert into vis_users (f_user, f_password) values ('1', '1')
--insert into vis_users (f_user, f_password) values ('2', '2')

-- ���������� ������� vis_visitors
delete vis_visitors where f_visitor_id <> '0'
insert into vis_visitors values ( '2', '21', '41', '������', '�����', '�����������', '1234', '123456', '1-���-2010', '���-��', '666', '', 'Y', '1', '', '����. ���.', 'Y', 'N', 'Y', 'F', 'N', '29-���-2017 13:12:22', '22', '1', '25-���-2017 17:50:53', '2', '2', '������ �. �.', 'N', '')
insert into vis_visitors values ( '21', '', '', '������', '����', '��������', '', '', '', '', '', '', 'Y', '', '', '', 'N', 'N', 'N', 'N', 'N', '29-���-2017 10:53:13', '22', '41', '29-���-2017 10:53:13', '22', '', '������ �. �.', 'N', '')
insert into vis_visitors values ( '22', '', '', '����', '������', '', '', '', '', '', '', '', 'Y', '22', '', '��������� ������', 'Y', 'Y', 'Y', 'N', 'N', '29-���-2017 12:06:05', '22', '1', '29-���-2017 12:03:59', '22', '47', '���� �.', 'N', '')
insert into vis_visitors values ( '23', '1', '1', '������', '����', '�����������', '', '', '', '', '', '', 'Y', '', '', '', 'N', 'N', 'N', 'N', 'N', '29-���-2017 12:29:46', '22', '42', '29-���-2017 12:29:46', '22', '', '������ �. �.', 'N', '')

-- ���������� ������� vis_visits
delete vis_visits
insert into vis_visits values ( '1', '3', '2', '20-���-2017 11:12:51', '13-���-2017 0:22:20', '', '20-���-2017', '28-���-2017', '', 'Y', '20-���-2017 11:12:51', '2', '', '2', '13-���-2017 0:22:20', '3', '')
insert into vis_visits values ( '21', '3', '2', '13-���-2017 0:22:20', '29-���-2017 12:51:50', '', '20-���-2017', '31-���-2025', '0', 'Y', '13-���-2017 0:22:20', '2', '', '22', '29-���-2017 12:51:50', '3', '')
insert into vis_visits values ( '41', '1', '21', '29-���-2017 12:52:23','','', '29-���-2017', '29-���-2017', '', 'N', '29-���-2017 12:52:23', '22','','','','3', '')

-- ���������� ������� vis_zone_types
delete vis_zone_types
insert into vis_zone_types values ( '1', '������� ����', 'N', '22-���-2003 13:23:07', '-1', '')
insert into vis_zone_types values ( '2', '�������� ��� �����������', 'N', '22-���-2003 13:25:41', '-1', '')
insert into vis_zone_types values ( '3', '�����������', 'N', '22-���-2003 13:25:42', '-1', '')
insert into vis_zone_types values ( '4', '�������� ��� �����������', 'N', '22-���-2003 13:25:42', '-1', '')
insert into vis_zone_types values ( '5', '�������� �������������� ����', 'N', '22-���-2003 13:25:42', '-1', '')
insert into vis_zone_types values ( '6', '�������� ��� ����������� �������', 'N', '22-���-2003 13:25:42', '-1', '')
insert into vis_zone_types values ( '8', '������', 'N', '22-���-2003 13:25:43', '-1', '')
insert into vis_zone_types values ( '7', '�������� ��� �����������', 'N', '22-���-2003 13:25:44', '-1', '')

-- ���������� ������� vis_zones
delete vis_zones
insert into vis_zones (F_ZONE_ID, F_ZONE_TYPE_ID, F_ZONE_NAME, F_DELETED, F_REC_DATE, F_REC_OPERATOR, F_ZONE_NUM, F_MONDAY_TIME_BEG_1, F_MONDAY_TIME_BEG_2, F_MONDAY_TIME_END_1, F_MONDAY_TIME_END_2, F_TUESDAY_TIME_BEG_1, F_TUESDAY_TIME_BEG_2, F_TUESDAY_TIME_END_1, F_TUESDAY_TIME_END_2, F_WHENSDAY_TIME_BEG_1, F_WHENSDAY_TIME_BEG_2, F_WHENSDAY_TIME_END_1, F_WHENSDAY_TIME_END_2, F_THURSDAY_TIME_BEG_1, F_THURSDAY_TIME_BEG_2, F_THURSDAY_TIME_END_1, F_THURSDAY_TIME_END_2, F_FRIDAY_TIME_BEG_1, F_FRIDAY_TIME_BEG_2, F_FRIDAY_TIME_END_1, F_FRIDAY_TIME_END_2, F_SATURDAY_TIME_BEG_1, F_SATURDAY_TIME_END_1, F_SUNDAY_TIME_BEG_1, F_SUNDAY_TIME_END_1, F_HOLIDAY_TIME_BEG_1, F_HOLIDAY_TIME_END_1, F_FIRST_CHECK, F_FECOND_CHECK, F_THIRD_CHECK, F_LAST_CHECK) values ( '1', '1', '���������', 'N', '29-���-2017 13:13:44', '22', '1',  '30-���-1899', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', 'N', 'Y', 'Y', 'Y')
insert into vis_zones (F_ZONE_ID, F_ZONE_TYPE_ID, F_ZONE_NAME, F_DELETED, F_REC_DATE, F_REC_OPERATOR, F_ZONE_NUM, F_MONDAY_TIME_BEG_1, F_MONDAY_TIME_BEG_2, F_MONDAY_TIME_END_1, F_MONDAY_TIME_END_2, F_TUESDAY_TIME_BEG_1, F_TUESDAY_TIME_BEG_2, F_TUESDAY_TIME_END_1, F_TUESDAY_TIME_END_2, F_WHENSDAY_TIME_BEG_1, F_WHENSDAY_TIME_BEG_2, F_WHENSDAY_TIME_END_1, F_WHENSDAY_TIME_END_2, F_THURSDAY_TIME_BEG_1, F_THURSDAY_TIME_BEG_2, F_THURSDAY_TIME_END_1, F_THURSDAY_TIME_END_2, F_FRIDAY_TIME_BEG_1, F_FRIDAY_TIME_BEG_2, F_FRIDAY_TIME_END_1, F_FRIDAY_TIME_END_2, F_SATURDAY_TIME_BEG_3, F_SATURDAY_TIME_BEG_4, F_SATURDAY_TIME_END_3, F_SATURDAY_TIME_END_4, F_SUNDAY_TIME_BEG_3, F_SUNDAY_TIME_BEG_4, F_SUNDAY_TIME_END_3, F_SUNDAY_TIME_END_4, F_HOLIDAY_TIME_BEG_3, F_HOLIDAY_TIME_BEG_4, F_HOLIDAY_TIME_END_3,  F_HOLIDAY_TIME_END_4, F_FIRST_CHECK, F_FECOND_CHECK, F_THIRD_CHECK, F_LAST_CHECK ) values ( '21', '2', '������� 1-�� �����', 'N', '29-���-2017 13:17:03', '22', '2',  '30-���-1899 8:30:00', '30-���-1899', '30-���-1899 20:00:00', '30-���-1899', '30-���-1899 8:30:00', '30-���-1899', '30-���-1899 20:00:00', '30-���-1899', '30-���-1899 8:30:00', '30-���-1899', '30-���-1899 20:00:00', '30-���-1899', '30-���-1899 8:30:00', '30-���-1899', '30-���-1899 20:00:00', '30-���-1899', '30-���-1899 8:30:00', '30-���-1899', '30-���-1899 20:00:00', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899', '30-���-1899', 'N', 'Y', 'Y', 'Y')
insert into vis_zones (F_ZONE_ID, F_ZONE_TYPE_ID, F_ZONE_NAME, F_DELETED, F_REC_DATE, F_REC_OPERATOR, F_ZONE_NUM, F_MONDAY_TIME_BEG_1, F_MONDAY_TIME_BEG_2, F_MONDAY_TIME_END_1, F_MONDAY_TIME_END_2, F_TUESDAY_TIME_BEG_1, F_TUESDAY_TIME_BEG_2, F_TUESDAY_TIME_END_1, F_TUESDAY_TIME_END_2, F_WHENSDAY_TIME_BEG_1, F_WHENSDAY_TIME_BEG_2, F_WHENSDAY_TIME_END_1, F_WHENSDAY_TIME_END_2, F_THURSDAY_TIME_BEG_1, F_THURSDAY_TIME_BEG_2, F_THURSDAY_TIME_END_1, F_THURSDAY_TIME_END_2, F_FRIDAY_TIME_BEG_1, F_FRIDAY_TIME_BEG_2, F_FRIDAY_TIME_END_1, F_FRIDAY_TIME_END_2, F_FIRST_CHECK, F_FECOND_CHECK, F_THIRD_CHECK, F_LAST_CHECK) values ( '22', '4', '�����������', 'N', '29-���-2017 13:17:19', '22', '3',  '30-���-1899 7:00:00', '30-���-1899', '30-���-1899 23:00:00', '30-���-1899', '30-���-1899 7:00:00', '30-���-1899', '30-���-1899 23:00:00', '30-���-1899', '30-���-1899 7:00:00', '30-���-1899', '30-���-1899 23:00:00', '30-���-1899', '30-���-1899 7:00:00', '30-���-1899', '30-���-1899 23:00:00', '30-���-1899', '30-���-1899 7:00:00', '30-���-1899', '30-���-1899 23:00:00', '30-���-1899', 'N', 'Y', 'Y', 'Y')
insert into vis_zones (F_ZONE_ID, F_ZONE_TYPE_ID, F_ZONE_NAME, F_DELETED, F_REC_DATE, F_REC_OPERATOR, F_ZONE_NUM, F_MONDAY_TIME_BEG_1, F_MONDAY_TIME_END_1, F_TUESDAY_TIME_BEG_1, F_TUESDAY_TIME_END_1, F_WHENSDAY_TIME_BEG_1, F_WHENSDAY_TIME_END_1, F_THURSDAY_TIME_BEG_1, F_THURSDAY_TIME_END_1, F_FRIDAY_TIME_BEG_1, F_FRIDAY_TIME_END_1, F_SATURDAY_TIME_BEG_1, F_SATURDAY_TIME_END_1, F_SUNDAY_TIME_BEG_1, F_SUNDAY_TIME_END_1, F_HOLIDAY_TIME_BEG_1, F_HOLIDAY_TIME_END_1, F_FIRST_CHECK, F_FECOND_CHECK, F_THIRD_CHECK, F_LAST_CHECK) values ( '41', '6', '������� ���������', 'N', '29-���-2017 13:14:18', '22', '4',  '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', 'N', 'Y', 'Y', 'Y')
insert into vis_zones (F_ZONE_ID, F_ZONE_TYPE_ID, F_ZONE_NAME, F_DELETED, F_REC_DATE, F_REC_OPERATOR, F_ZONE_NUM, F_MONDAY_TIME_BEG_1, F_MONDAY_TIME_END_1, F_TUESDAY_TIME_BEG_1, F_TUESDAY_TIME_END_1, F_WHENSDAY_TIME_BEG_1, F_WHENSDAY_TIME_END_1, F_THURSDAY_TIME_BEG_1, F_THURSDAY_TIME_END_1, F_FRIDAY_TIME_BEG_1, F_FRIDAY_TIME_END_1, F_SATURDAY_TIME_BEG_1, F_SATURDAY_TIME_END_1, F_SUNDAY_TIME_BEG_1, F_SUNDAY_TIME_END_1, F_HOLIDAY_TIME_BEG_1, F_HOLIDAY_TIME_END_1, F_FIRST_CHECK, F_FECOND_CHECK, F_THIRD_CHECK, F_LAST_CHECK) values ( '42', '7', '����������', 'N', '29-���-2017 13:14:47', '22', '5',  '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', '30-���-1899', '30-���-1899 23:59:00', 'N', 'Y', 'Y', 'Y')

-- ���������� ������� vis_zones_order_elements
delete vis_zones_order_elements
insert into vis_zones_order_elements values ( '64', '62', '42')
insert into vis_zones_order_elements values ( '3', '3', '1')
insert into vis_zones_order_elements values ( '41', '41', '22')
insert into vis_zones_order_elements values ( '61', '2', '1')
insert into vis_zones_order_elements values ( '63', '61', '1')
insert into vis_zones_order_elements values ( '71', '67', '21')
insert into vis_zones_order_elements values ( '69', '66', '42')
insert into vis_zones_order_elements values ( '77', '65', '1')
insert into vis_zones_order_elements values ( '78', '65', '22')