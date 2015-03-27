use [TrelloDB]
set dateformat dmy

delete from [TrelloDB].dbo.[Card]
delete from [TrelloDB].dbo.List
delete from [TrelloDB].dbo.Board

DBCC CHECKIDENT (Board, reseed, 0)
DBCC CHECKIDENT (List, reseed, 0)
DBCC CHECKIDENT ([Card], reseed, 0)

---------------------------------------------------------------------------------------------------------------------------------

/*Tabela Board*/
insert into Board (Name, Discription) values
 ('Caetano',	'Quadro do Caetano')
,('Pedro',		'Quadro do Pedro')
,('Joao',		'Quadro do Joao')

--select * from Board

---------------------------------------------------------------------------------------------------------------------------------

/*Tabela List*/
insert into [List] (Name,Lix,BoardId) values
 ('LS',		1,	1)
,('AVE',	2,	1)
,('PSC',	3,	1)
,('LS',		1,	2)
,('AVE',	2,	2)
,('SO',		3,	2)
,('LS',		1,	3)
,('PC',		2,	3)
,('PI',		3,	3)

--select * from List

---------------------------------------------------------------------------------------------------------------------------------

/*Tabela [Card]*/
insert into [Card] 
(Name, Discription, CreationDate, DueDate, Cix, BoardId, ListId) 
values
 ('Etapa1',  'Primeira etapa de LS',	GETDATE(),	'14-03-2016',	1,	1,	1)
,('Etapa2',  'Segunda etapa de LS',		GETDATE(),	'14-03-2016',	2,	1,	1)
,('Etapa3',  'Terceira etapa de LS',	GETDATE(),	'14-03-2016',	3,	1,	1)
,('Serie1',  'Primeira serie de AVE',	GETDATE(),	'14-03-2016',	1,	1,	2)
,('Serie2',  'Segunda serie de AVE',	GETDATE(),	'14-03-2016',	2,	1,	2)
,('Serie3',  'Teceira serie de AVE',	GETDATE(),	'14-03-2016',	3,	1,	2)
,('Serie1',  'Primeira serie PSC',		GETDATE(),	'14-03-2016',	1,	1,	3)
,('Serie2',  'Segundoa serie PSC',		GETDATE(),	'14-03-2016',	2,	1,	3)
,('Serie3',  'Teceira serie PSC',		GETDATE(),	'14-03-2016',	3,	1,	3)

,('Etapa1',  'Primeira etapa de LS',	GETDATE(),	'14-03-2016',	1,	2,	4)
,('Etapa2',  'Segunda etapa de LS',		GETDATE(),	'14-03-2016',	2,	2,	4)
,('Etapa3',  'Teceira etapa de LS',		GETDATE(),	'14-03-2016',	3,	2,	4)
,('Serie1',  'Primeira serie de AVE',	GETDATE(),	'14-03-2016',	1,	2,	5)
,('Serie2',  'Segunda serie de AVE',	GETDATE(),	'14-03-2016',	2,	2,	5)
,('Serie3',  'Teceira serie de AVE',	GETDATE(),	'14-03-2016',	3,	2,	5)
,('Serie1',  'Primeira serie de SO',	GETDATE(),	'14-03-2016',	1,	2,	6)
,('Serie2',  'Segunda serie de SO',		GETDATE(),	'14-03-2016',	2,	2,	6)
,('Serie3',  'Teceira serie de SO',		GETDATE(),	'14-03-2016',	3,	2,	6)

,('Etapa1',  'Primeira etapa de LS',	GETDATE(),	'14-03-2016',	1,	3,	7)
,('Etapa2',  'Segunda etapa de LS',		GETDATE(),	'14-03-2016',	2,	3,	7)
,('Etapa3',  'Teceira etapa de LS',		GETDATE(),	'14-03-2016',	3,	3,	7)
,('Serie1',  'Primeira serie de PC',	GETDATE(),	'14-03-2016',	1,	3,	8)
,('Serie2',  'Segunda serie de PC',		GETDATE(),	'14-03-2016',	2,	3,	8)
,('Serie3',  'Teceira serie de PC',		GETDATE(),	'14-03-2016',	3,	3,	8)
,('Serie1',  'Primeira serie de PI',	GETDATE(),	'14-03-2016',	1,	3,	9)
,('Serie2',  'Segunda serie de PI',		GETDATE(),	'14-03-2016',	2,	3,	9)
,('Serie3',  'Teceira serie de PI',		GETDATE(),	'14-03-2016',			3,	3,	9)
--select * from [Card]

---------------------------------------------------------------------------------------------------------------------------------
/*Tabela Tag*/
/*
insert into Tag values('T1','Tag1','Blue');
insert into Tag values('T2','Tag2','Red');
insert into Tag values('T3','Tag3','Green');
insert into Tag values('T4','Tag4','Brown');
insert into Tag values('T5','Tag5','Yellow');
insert into Tag values('T6','Tag6','Pink');
insert into Tag values('T7','Tag7','Orange');
insert into Tag values('T8','Tag8','Violet');
insert into Tag values('T9','Tag9','White');

--select * from Tag

---------------------------------------------------------------------------------------------------------------------------------
/*Tabela CardTag*/
insert into CardTag values('T1','Caetano', 1);
insert into CardTag values('T2','Caetano', 1);
insert into CardTag values('T2','Caetano', 3);
insert into CardTag values('T4','Pedro',  10);
insert into CardTag values('T5','Pedro',  11);
insert into CardTag values('T6','Pedro',  12);
insert into CardTag values('T7','Joao',   19);
insert into CardTag values('T8','Joao',   20);
insert into CardTag values('T9','Joao',   20)
*/
--select * from CardTag
