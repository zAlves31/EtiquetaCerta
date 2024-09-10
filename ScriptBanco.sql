-- Cria��o do banco de dados
CREATE DATABASE EtiquetaCerta;
GO

USE EtiquetaCerta;
GO

-- Cria��o da tabela ConservationProcess
CREATE TABLE ConservationProcess (
    id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    name VARCHAR(100),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE() -- SQL Server n�o tem ON UPDATE CURRENT_TIMESTAMP
);
GO

-- Cria��o da tabela Symbology
CREATE TABLE Symbology (
    id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    id_process UNIQUEIDENTIFIER,
    name VARCHAR(100),
    description VARCHAR(200),
    url VARCHAR(200),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    
    FOREIGN KEY (id_process) REFERENCES ConservationProcess(id)
);
GO

-- Cria��o da tabela Legislation
CREATE TABLE Legislation (
    id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    name VARCHAR(100),
    official_language VARCHAR(100),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);
GO

-- Cria��o da tabela Label
CREATE TABLE Label (
    id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    name VARCHAR(100),
    id_legislation UNIQUEIDENTIFIER,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    
    FOREIGN KEY (id_legislation) REFERENCES Legislation(id)
);
GO

-- Cria��o da tabela LabelSymbology
CREATE TABLE LabelSymbology (
    id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    id_symbology UNIQUEIDENTIFIER,
    id_label UNIQUEIDENTIFIER,
    
    FOREIGN KEY (id_symbology) REFERENCES Symbology(id),
    FOREIGN KEY (id_label) REFERENCES Label(id)
);
GO

-- Cria��o da tabela SymbologyTranslate
CREATE TABLE SymbologyTranslate (
    id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    id_symbology UNIQUEIDENTIFIER,
    id_legislation UNIQUEIDENTIFIER,
    symbology_translate VARCHAR(100),

    FOREIGN KEY (id_symbology) REFERENCES Symbology(id),
    FOREIGN KEY (id_legislation) REFERENCES Legislation(id)
);
GO

-- Cria��o da tabela ProcessInLegislation
CREATE TABLE ProcessInLegislation (
    id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    id_process UNIQUEIDENTIFIER,
    id_legislation UNIQUEIDENTIFIER,

    FOREIGN KEY (id_process) REFERENCES ConservationProcess(id),
    FOREIGN KEY (id_legislation) REFERENCES Legislation(id)
);
GO

-- Inser��o de dados na tabela ConservationProcess
INSERT INTO ConservationProcess (name)
VALUES 
    ('Lavagem'),
    ('Alvejamento'),
    ('Secagem'),
    ('Passadoria'),
    ('Limpeza profissional');
GO

-- Inser��o de dados na tabela Symbology
INSERT INTO Symbology (id_process, name, description, url)
VALUES 
    ((SELECT id FROM ConservationProcess WHERE name = 'Lavagem'), 'lavagem-30-graus', 'Lavagem normal com temperatura m�xima de 30 graus celsius', 'https://www.maison-travaux.fr/wp-content/uploads/sites/8/2018/08/un-rincage-normal-et-un-essorage-reduit.gif'),
    ((SELECT id FROM ConservationProcess WHERE name = 'Lavagem'), 'lavagem-50-graus', 'Lavagem normal com temperatura m�xima de 50 graus celsius', 'https://thumbs.dreamstime.com/b/temperatura-da-lavagem-graus-de-s%C3%ADmbolo-%C3%ADcone-do-153330737.jpg'),
    ((SELECT id FROM ConservationProcess WHERE name = 'Alvejamento'), 'qualquer-alvejante', 'Qualquer alvejante � permitido', 'https://static.thenounproject.com/png/2014515-200.png'),
    ((SELECT id FROM ConservationProcess WHERE name = 'Alvejamento'), 'alvejantes-clorados', 'N�o � permitido alvejantes clorados, apenas com oxig�nio', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTD62-QBphtIQUX_tXFciBAoaaHhaBkKHMatpL_k0DdsKgoPOO7MA1RlB3QzLg1pflQLdE&usqp=CAU'),
    ((SELECT id FROM ConservationProcess WHERE name = 'Secagem'), 'secagem-tambor-temperatura-baixa', 'Poss�vel secagem em tambor com temperatura baixa', 'https://www.uma.com.br/images/icons/cuidados-lavagem/cuidados-28.png'),
    ((SELECT id FROM ConservationProcess WHERE name = 'Secagem'), 'secagem-tambor-temperatura-normal', 'Poss�vel secagem em tambor com temperatura normal', 'https://oneoffconfeccao.com.br/wp-content/uploads/2018/05/secagem1_oneoffconfeccao.png'),
    ((SELECT id FROM ConservationProcess WHERE name = 'Passadoria'), 'ferro-110', 'Temperatura m�xima da base do ferro de 110 �C', 'https://1.bp.blogspot.com/_yUJ5ZdEbVDI/TJ4z04QO17I/AAAAAAAAA8k/JRzOvgHtX8U/s1600/bolxnxhku160vnohawvv5i45v.jpg'),
    ((SELECT id FROM ConservationProcess WHERE name = 'Passadoria'), 'ferro-150', 'Temperatura m�xima da base do ferro de 150 �C', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSePXiiQmeK1mHYZIjs3nI3Hm7JDz-H5XEpBgO0A00zxmEt21gZD2dPmw5BA6CTUpORe3U&usqp=CAU'),
    ((SELECT id FROM ConservationProcess WHERE name = 'Limpeza profissional'), 'limpeza-seco-tetracloroetileno-normal', 'Limpeza a seco profissional com tetracloroetileno em processo normal', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRVQLtdoWcPaJuW2FPou_0S3yVaPL4sUWWOH2zpUbBLQGSW2Buf26t6wnGll4PaPhztA2Q&usqp=CAU'),
    ((SELECT id FROM ConservationProcess WHERE name = 'Limpeza profissional'), 'limpeza-seco-tetracloroetileno-suave', 'Limpeza a seco profissional com tetracloroetileno em processo suave', 'https://oneoffconfeccao.com.br/wp-content/uploads/2018/06/simbolos-cuidados-oneoff-confeccao-lavagem-profissional-3.png');
GO
