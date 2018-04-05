-- �������� �� VisitorsLogs �� T-SQL
create database VisitorsLogs;
go

use VisitorsLogs;
go

-- �������� vis_log
CREATE TABLE vis_log
    (f_log_id                      bigint NOT NULL IDENTITY(1,1),
	f_table_name                   VARCHAR(1000),  -- ���� �����, ���� ����� ������ �������� ������� �� ���� Visitors ��� ��������
	f_table_id                     int,            -- ���� �����, ���� ����� ������ id �� ������� ���� Visitors ��� ��������
	f_rec_operator                 int,            -- ���� �����, ���� ����� ������ id ������������ �� ������� ���� Visitors ��� ��������
	f_log_severety                 VARCHAR(50),    -- ����������� ������� - ����� ���� � ��������
	f_log_class                    VARCHAR(MAX),   -- �����, � ������� ��������� �������
    f_log_message                  VARCHAR(1000),
    f_rec_date                     DATE,
	f_comment                      VARCHAR(200))   -- �����������, �� ������ ������

ALTER TABLE vis_log
ADD PRIMARY KEY (f_log_id)
