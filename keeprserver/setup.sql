CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR (255) COMMENT 'User Name',
  email VARCHAR (255) COMMENT 'User Email',
  picture VARCHAR (255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: User Account',
  name VARCHAR (255) NOT NULL COMMENT 'Keep Name',
  description VARCHAR(255) NOT NULL COMMENT 'Keep Description',
  img VARCHAR(255) NOT NULL COMMENT 'Keep Img',
  views INT COMMENT 'Keep View Count',
  shares INT COMMENT 'Keep Shares Count',
  keeps INT COMMENT 'Keep Keeps Count',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'TS Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'TS Update',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: User Account',
  vaultId INT NOT NULL COMMENT 'FK: Vault Relationship',
  keepId INT NOT NULL COMMENT 'FK: Keep Relationship',
  davidId INT NOT NULL COMMENT 'FK: Owner Relationship',
  vaultkeepId INT NOT NULL COMMENT 'FK: Keep Relationship',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: User Account',
  name VARCHAR (255) NOT NULL COMMENT 'Vault Name',
  description VARCHAR(255) NOT NULL COMMENT 'Vault Description',
  img VARCHAR(255) COMMENT 'Keep Img',
  isPrivate BOOLEAN NOT NULL DEFAULT 0 COMMENT 'Vault Private Boolean,Default FALSE',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
DROP TABLE vaultkeeps;
SELECT
  v.*,
  a.*
FROM
  vaults v
  JOIN accounts a ON v.creatorId = a.id
WHERE
  v.id = 262;