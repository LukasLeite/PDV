# Banco de Dados - PDV_1 (SQLite)

Este repositório contém o arquivo de banco de dados `PDV_1.db`, criado com **SQLite**, para uso em um sistema de Ponto de Venda (PDV) desenvolvido em C# com Windows Forms.

---

## 🛠️ Informações do Banco

- **Nome do arquivo**: `PDV_1.db`
- **Tecnologia**: [SQLite](https://www.sqlite.org/index.html)
- **Tabelas principais**:
  - `Usuario`
  - `Produtos`
  - `Vendas`

---

## 📋 Estrutura das Tabelas

### 🔹 `Usuario`

```sql
CREATE TABLE Usuario (
    ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,
    Nome TEXT NOT NULL,
    Email TEXT,
    Telefone INTEGER NOT NULL,
    Sexo TEXT,
    ADM INTEGER DEFAULT 0,
    ATIVO INTEGER DEFAULT 1,
    DATA_CADASTRO TEXT DEFAULT (date('now'))
);
