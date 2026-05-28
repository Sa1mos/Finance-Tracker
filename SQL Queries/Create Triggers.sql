-- Function to automatically update the updated_at timestamp
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Trigger for Transaction_Categories table
CREATE TRIGGER update_transaction_categories_updated_at
BEFORE UPDATE ON Transaction_Categories
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

-- Trigger for Wallets table
CREATE TRIGGER update_wallets_updated_at
BEFORE UPDATE ON Wallets
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

-- Trigger for Transactions table
CREATE TRIGGER update_transactions_updated_at
BEFORE UPDATE ON Transactions
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

-- Trigger for Currencies table
CREATE TRIGGER update_currencies_updated_at
BEFORE UPDATE ON Currencies
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();