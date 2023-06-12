import random as r
from datetime import date


with open('Orders.csv', 'w', encoding='UTF-16') as f:
	lst = list()
	f.write('Id,UserId,Status\n')

	order_id = 1
	user_id = 1
	status = 'оплачен'

	max_iters = 1000000;

	while order_id < max_iters + 1:
		string = f'{order_id},{r.randint(1, max_iters)},{status}\n'
		lst.append(string)

		order_id += 1

	r.shuffle(lst)

	for i in range(max_iters):
		f.write(lst[i])
