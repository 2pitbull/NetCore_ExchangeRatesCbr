create table if not exists Currency(
	CurrencyId serial primary key,
	CbrId varchar(20) unique,
	RusName varchar(50),
	EngName varchar(50),
	Nominal int,
	ParentCode varchar(20)
);

create table if not exists ExchangeRate(
    ExchangeRateId serial primary key,
	Date date,
	CbrId varchar(20),
	NumCode varchar(3),
	CharCode varchar(3),
	Value numeric,
	VunitRate numeric,
	constraint fk_ExchangeRate_CbrId foreign key(CbrId) references Currency(CbrId)
);
