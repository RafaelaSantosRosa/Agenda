insert into TBTarefa
	(
		Titulo, 
		DataCriação, 
		DataConclusão, 
		PercentualConclusão,
		Prioridade
	)
	values 
	(
		  'TrabalhoFaculdade',
		  '06/10/2021',
		  '06/15/2021',
		  10,
		  1
	)

insert into TBTarefa
	(
		Titulo, 
		DataCriação, 
		DataConclusão, 
		PercentualConclusão
	)
	values 
	(
		  'Atividade 15',
		  '06/10/2021',
		  '06/15/2021',
		  25
	)

select * from TBTarefa

update TBTarefa 
	set	
		[Titulo] = 'TrabalhoFaculdade', 
		[DataCriação]='06/10/2021', 
		[DataConclusão] = '06/15/2021',
	    [PercentualConclusão] = 50,
		[Prioridade] = 1
	where 
		[Id] = 1
select * from TBTarefa

Delete from TBTarefa 
	where 
		[Id] = 1

select * from TBTarefa
	where
	[Id] = 5

select [Id], [Titulo], [DataCriação], [DataConclusão], [PercentualConclusão],  [Prioridade] from TBTarefa