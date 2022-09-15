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
  Foreign Key (creatorId) REFERENCES accounts (id)
)default charset utf8 COMMENT '';

DROP TABLE recipe;

INSERT INTO recipes
(picture, title, subtitle, category, creatorId)
VALUES
('https://images.unsplash.com/photo-1495546968767-f0573cca821e?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1331&q=80', 'cookbook', 'this is a cook book', 'random', '63238c2efe6a9c0c39e53393');

SELECT * FROM recipes r
JOIN accounts a ON a.id = r.creatorId;



