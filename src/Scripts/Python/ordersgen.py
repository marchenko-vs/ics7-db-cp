import random as r
from datetime import date


with open('Orders.csv', 'w', encoding='UTF-16') as f:
	f.write('Id,UserId,Status\n')

	order_id = 1
	user_id = 1
	status = 'оплачен'

	max_iters = 10000000;

	while order_id < max_iters + 1:
		f.write(f'{order_id},{user_id},{status}\n')

		order_id += 1
		user_id += 1
