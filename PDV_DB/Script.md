
## 📄 Banco de Dados - Script

### ✅ **Configuração inicial do SQLite**

```sql
PRAGMA foreign_keys = ON;
```

Ativa a verificação de integridade referencial no SQLite.

---

## ✅ **Tabelas e Chaves**

### 🟢 **Tabela: USER**

```sql
CREATE TABLE USER (
    id_user INTEGER PRIMARY KEY AUTOINCREMENT,
    nome TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL,
    senha TEXT NOT NULL
);
```

- **Primary Key (PK):** `id_user`  
- **Foreign Keys (FK):** Nenhuma  

---

### 🟢 **Tabela: PRODUTO**

```sql
CREATE TABLE PRODUTO (
    id_produto INTEGER PRIMARY KEY AUTOINCREMENT,
    nome TEXT NOT NULL,
    preco REAL NOT NULL,
    estoque INTEGER NOT NULL
);
```

- **Primary Key (PK):** `id_produto`  
- **Foreign Keys (FK):** Nenhuma  

---

### 🟢 **Tabela: CAIXA**

```sql
CREATE TABLE CAIXA (
    id_caixa INTEGER PRIMARY KEY AUTOINCREMENT,
    descricao TEXT NOT NULL,
    data_abertura TEXT NOT NULL, 
    data_fechamento TEXT
);
```

- **Primary Key (PK):** `id_caixa`  
- **Foreign Keys (FK):** Nenhuma  

---

### 🟢 **Tabela: VENDA**

```sql
CREATE TABLE VENDA (
    id_venda INTEGER PRIMARY KEY AUTOINCREMENT,
    id_user INTEGER NOT NULL,
    id_caixa INTEGER NOT NULL,
    data_venda TEXT NOT NULL,
    valor_total REAL NOT NULL,
    FOREIGN KEY (id_user) REFERENCES USER(id_user),
    FOREIGN KEY (id_caixa) REFERENCES CAIXA(id_caixa)
);
```

- **Primary Key (PK):** `id_venda`  
- **Foreign Keys (FK):**  
  - `id_user` → `USER(id_user)`  
  - `id_caixa` → `CAIXA(id_caixa)`  

---

### 🟢 **Tabela: VENDA_PRODUTO**

```sql
CREATE TABLE VENDA_PRODUTO (
    id_venda INTEGER NOT NULL,
    id_produto INTEGER NOT NULL,
    quantidade INTEGER NOT NULL,
    preco_unitario REAL NOT NULL,
    PRIMARY KEY (id_venda, id_produto),
    FOREIGN KEY (id_venda) REFERENCES VENDA(id_venda),
    FOREIGN KEY (id_produto) REFERENCES PRODUTO(id_produto)
);
```

- **Primary Key (PK):**  
  `(id_venda, id_produto)`  

- **Foreign Keys (FK):**  
  - `id_venda` → `VENDA(id_venda)`  
  - `id_produto` → `PRODUTO(id_produto)`  

---

