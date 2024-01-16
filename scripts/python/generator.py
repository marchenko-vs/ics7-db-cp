import random as r
import datetime as d


planes = list()  # plane id, economy, first, business

flight_num = 1
available  = 1
places = 'ABCDEFGHJK'
ticket_id = 1

first_price_min = 15500
first_price_max = 25000
business_price_min = 7500
business_price_max = 15000
economy_price_min = 3000
economy_price_max = 7000

points = ['Архангельск', 'Астрахань', 'Барнаул', 'Москва (Домодедово)', 'Санкт-Петербург', 'Рязань', 'Саратов', 'Москва (Шереметьево)']

with open('../Data/Planes.csv', 'r', encoding='UTF-16') as f:
	line = f.readline()

	while line:
		line = f.readline()
		lst = line.strip().split(',')
		if lst[0] != '':
			planes.append([lst[0], int(lst[3]), int(lst[4]), int(lst[5])])

f2 = open('../Data/Tickets.csv', 'w', encoding='UTF-16')
f2.write('Id,FlightId,OrderId,Row,Place,Class,Refund,Price\n')

with open('../Data/Flights.csv', 'w', encoding='UTF-16') as f:
	f.write('Id,PlaneId,DeparturePoint,ArrivalPoint,DepartureDateTime,ArrivalDateTime\n')

	for i in range(flight_num):
		plane_num = r.randint(0, len(planes) - 1)
		plane_id = planes[plane_num][0]
		economy_class_num = planes[plane_num][1]
		first_class_num = planes[plane_num][2]
		business_class_num = planes[plane_num][3]

		departure_point = points[r.randint(0, len(points) - 1)]
		arrival_point = points[r.randint(0, len(points) - 1)]

		while departure_point == arrival_point:
			arrival_point = points[r.randint(0, len(points) - 1)]

		departure_date_time = d.datetime.now()
		time_change = d.timedelta(minutes=r.randint(45, 180))
		arrival_date_time = departure_date_time + time_change

		f.write(f'{i + 1},{plane_id},{departure_point},{arrival_point},{departure_date_time},{arrival_date_time}\n')

		row = 1
		place_ind = 0
		place = places[place_ind]

		class_name = 'первый'

		for j in range(first_class_num):
			f2.write(f'{ticket_id},{i + 1},0,{row},{place},{class_name},{r.randint(0, 1)},{r.randint(first_price_min, first_price_max)}\n')
			ticket_id += 1
			place_ind += 1
			if place_ind == len(places):
				place_ind = 0
				row += 1
			place = places[place_ind]

		place_ind = 0
		place = places[place_ind]

		class_name = 'бизнес'

		for j in range(business_class_num):
			f2.write(f'{ticket_id},{i + 1},0,{row},{place},{class_name},{r.randint(0, 1)},{r.randint(business_price_min, business_price_max)}\n')
			ticket_id += 1
			place_ind += 1
			if place_ind == len(places):
				place_ind = 0
				row += 1
			place = places[place_ind]

		place_ind = 0
		place = places[place_ind]

		class_name = 'эконом'

		for j in range(economy_class_num):
			f2.write(f'{ticket_id},{i + 1},0,{row},{place},{class_name},{r.randint(0, 1)},{r.randint(economy_price_min, economy_price_max)}\n')
			ticket_id += 1
			place_ind += 1
			if place_ind == len(places):
				place_ind = 0
				row += 1
			place = places[place_ind]

f2.close()
