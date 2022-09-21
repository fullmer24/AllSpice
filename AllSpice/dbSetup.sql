CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
SELECT * FROM accounts;

CREATE TABLE IF NOT EXISTS recipes(
  id INT AUTO_INCREMENT PRIMARY KEY,
  picture VARCHAR(255) DEFAULT 'https://images.unsplash.com/photo-1495546968767-f0573cca821e?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1331&q=80',
  title VARCHAR(255) NOT NULL,
  subtitle VARCHAR(255) NOT NULL, 
  category VARCHAR(255) NOT NULL DEFAULT 'random',
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id)
)default charset utf8 COMMENT '';
SELECT * FROM recipes;

CREATE TABLE IF NOT EXISTS ingredients(
  id INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(255) NOT NULL,
  quantity INT NOT NULL,
  recipeId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id),
  FOREIGN KEY (recipeId) REFERENCES recipes (id)
)default charset utf8 COMMENT '';
SELECT * FROM ingredients;

CREATE TABLE IF NOT EXISTS steps(
  id INT AUTO_INCREMENT PRIMARY KEY,
  position INT NOT NULL,
  body VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id),
  FOREIGN KEY (recipeId) REFERENCES recipes (id)
)default charset utf8 COMMENT '';

SELECT * FROM steps;

CREATE TABLE IF NOT EXISTS favorites(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  recipeId INT NOT NULL,
  accountId VARCHAR(255) NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

-- NOTE create
INSERT INTO favorites
(recipeId, accountId)
VALUES
(5, '63238c2efe6a9c0c39e53393');

-- NOTE get
SELECT 
  a.*,
  f.*
  FROM favorites f
  JOIN accounts a ON a.id = f.accountId
  WHERE f.recipeId = 3;

-- NOTE delete
  DELETE FROM favorites 
  WHERE favorites.id = 3;


SELECT  
  r.*,  
  f.*
FROM favorites f
JOIN recipes r ON r.creatorId = f.accountId
WHERE f.accountId = "63238c2efe6a9c0c39e53393";