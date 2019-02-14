--Criar banco de dados
CREATE DATABASE SVIGUFO;

/*Abre o banco de dados*/
USE SVIGUFO;

--Cria uma tabela com tipos de evento
CREATE TABLE TIPO_EVENTO(
	ID BIGINT IDENTITY PRIMARY KEY, --Setando ID como chave primaria
	NOME VARCHAR(100) UNIQUE NOT NULL--Seta o nome do tipo de evento , que dever� ser unico e nao poder� ser nulo
);
/*Cria a tabela com as institui��es*/
CREATE TABLE INSTITUICAO(
	ID BIGINT IDENTITY PRIMARY KEY,
	NOME_FANTASIA VARCHAR(250),/**/
	RAZAO_SOCIAL VARCHAR(250) NOT NULL,
	CNPJ CHAR(14) UNIQUE NOT NULL,
	LOGRADOURO VARCHAR(300),
	CEP CHAR(8) NOT NULL,
	UF CHAR(2) NOT NULL,/*Seta uma string com valor fixo de 2 caracteres (utilizado quando se tem certeza absoluta do tamanho da string inserida)*/
	CIDADE VARCHAR(250) NOT NULL
);

CREATE TABLE EVENTOS(
	/*PRIMARY KEY porque ser� reconhecida a partir deste valor*/
	ID BIGINT IDENTITY PRIMARY KEY,
	NOME VARCHAR(250) NOT NULL,
	DESCRICAO TEXT NOT NULL,/*TEXT = um varchar grande e sem limite de caracteres*/
	DATA_EVENTO DATETIME NOT NULL,
	ACESSO_LIVRE BIT DEFAULT(1),
	ID_INSTITUICAO BIGINT,
	ID_TIPO_EVENTO BIGINT,
	FOREIGN KEY (ID_INSTITUICAO) REFERENCES INSTITUICAO(ID),/*Seta uma variavel como uma chave estrangeira se referindo a chave primaria de outra tabela*/
	FOREIGN KEY (ID_TIPO_EVENTO) REFERENCES TIPO_EVENTO(ID)
);

CREATE TABLE USUARIOS(
	ID BIGINT IDENTITY PRIMARY KEY,
	NOME VARCHAR(250) NOT NULL,
	EMAIL VARCHAR(250) NOT NULL UNIQUE,
	SENHA VARCHAR(50) NOT NULL,
	ID_TIPO_USUARIO BIGINT,
	FOREIGN KEY (ID_TIPO_USUARIO) REFERENCES TIPO_USUARIO(ID)
);

CREATE TABLE TIPO_USUARIO(
	ID BIGINT IDENTITY PRIMARY KEY,
	NOME VARCHAR(50)
);

CREATE TABLE CONVITES(
	ID BIGINT IDENTITY PRIMARY KEY,
	ID_USUARIO BIGINT FOREIGN KEY REFERENCES USUARIOS(ID),--MANEIRA DECENTE DE CRIAR CHAVE ESTRANGEIRA ;-;
	ID_EVENTO BIGINT FOREIGN KEY REFERENCES EVENTOS(ID),
	COMPARECERA BIT DEFAULT(1)
);

--ADICIONA UMA NOVA COLUNA PARA A TABELA
ALTER TABLE USUARIOS ADD ATIVO CHAR(1);
--REMOVE A NOVA COLUNA DA TABELA
ALTER TABLE USUARIOS DROP COLUMN ATIVO;