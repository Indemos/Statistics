# Maximum adverse excursion 

Every open position continuously experiences profit fluctuations. 
Every trade has its maximal profit and its maximal loss during the period between its opening and closing. 
MAE shows the maximal price movement in an adverse direction or potential loss that might happen. 
If securities measured in different units were traded, it is better to express results in money.

$$ MAE = \frac{ \sum\limits_{i=0}^{N}(V_i - MaxLoss_i) }{N} $$ 

- V - Gain from transaction 
- MaxLoss - Max unrealized loss during the transaction
- N - Number of transactions
