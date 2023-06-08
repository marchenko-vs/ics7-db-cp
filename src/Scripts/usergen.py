import random as r
from datetime import date


num_of_rows = 5

alphabet = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890'
email_alphabet = 'abcdefghijklmnopqrstuvwxyz1234567890_'
roles = ['admin', 'moderator', 'customer']
domains = ['gmail.com', 'yandex.ru', 'ya.ru', 'bk.ru', 'mail.ru', 'yahoo.com', 'apple.com', 'bmstu.ru']

with open('Users.csv', 'w', encoding='UTF-16') as f:
	f.write('Id,Role,Email,Password,FirstName,LastName,RegDate\n')

	user_id = 1
	reg_date = date.today()

	for i in range(num_of_rows):
		email = ''
		password = ''
		first_name = ''
		last_name = ''

		role = roles[r.randint(0, 2)]

		email_len_1 = r.randint(15, 20)
		email_len_2 = r.randint(4, 8)

		for _ in range(email_len_1):
			email += alphabet[r.randint(0, len(alphabet) - 1)]

		email += '@'

		domain_index = r.randint(0, len(domains) - 1)

		email += domains[domain_index]

		password_len = r.randint(16, 32)

		for _ in range(password_len):
			password += alphabet[r.randint(0, len(alphabet) - 1)]

		name_len = r.randint(5, 15)

		for _ in range(name_len):
			first_name += alphabet[r.randint(0, len(alphabet) - 1)]

		for _ in range(name_len):
			last_name += alphabet[r.randint(0, len(alphabet) - 1)]

		f.write(f'{user_id},{role},{email},{password},{first_name},{last_name},{reg_date}\n')

		progress = user_id * 100 / num_of_rows
		print(f'{round(progress, 2)}%', end='\r')

		user_id += 1

print('\nReady')
