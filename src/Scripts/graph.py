import pandas as pd
from matplotlib import pyplot as plt

d = {'Без индекса': [468, 435, 471, 757, 1642], 'С использованием кластеризованного индекса': [440, 415, 421, 414, 472]}
df = pd.DataFrame(d, index=[100, 1000, 10000, 100000, 1000000])

df.plot(style=['-o', '-rx'], use_index=True)
plt.xlabel('Количество строк в таблице')
plt.ylabel('Время выполнения запроса, мс')
plt.savefig("graph-01.pdf", format="pdf", bbox_inches="tight")

# d = {'Без индекса': [422, 483, 457, 559, 1454, 3816, 30458], 'С использованием кластеризованного индекса': [423, 468, 428, 435, 427, 485, 511]}
# df = pd.DataFrame(d, index=[10, 100, 1000, 10000, 50000, 100000, 250000])
# df.plot(style=['-o', '-rx'], use_index=True)
# plt.xlabel('Количество строк в таблице')
# plt.ylabel('Время выполнения запроса, мс')
# plt.savefig("graph-02.pdf", format="pdf", bbox_inches="tight")

d = {'Без индекса': [495, 644, 2136, 5978, 50571], 'С использованием кластеризованного индекса': [442, 448, 440, 498, 524]}
df = pd.DataFrame(d, index=[100, 1000, 10000, 100000, 1000000])
df.plot(style=['-o', '-rx'], use_index=True)
plt.xlabel('Количество строк в таблице')
plt.ylabel('Время выполнения запроса, мс')
plt.savefig("graph-02.pdf", format="pdf", bbox_inches="tight")
