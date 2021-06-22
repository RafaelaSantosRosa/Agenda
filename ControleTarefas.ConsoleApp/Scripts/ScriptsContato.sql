insert into TBContato
	(
		Nome, 
		Telefone, 
		Email, 
		Empresa,
		Cargo
	)
	values 
	(
		  'Rafaela',
		  '1234',
		  'rafa@gmail',
		  'ndd',
		  'estagio'		  
	)

select * from TBContato

update TBContato 
	set	
		[Nome] = 'Zezo', 
		[Telefone]='4321', 
		[Email] = 'zezo@gmail',
	    [Empresa] = 'ndd',
		[Cargo] = 'estagio'
	where 
		[Id] = 1
select * from TBContato

Delete from TBContato 
	where 
		[Id] = 1

select * from TBContato

select [Id], [Nome], [Telefone], [Email], [Empresa],  [Cargo] from TBContato
	where
		[Cargo] = @Cargo