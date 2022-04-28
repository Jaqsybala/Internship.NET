--task 1
WITH users (
	user_id,
	action,
	DATE
	)
AS (
	SELECT 1,
		'start',
		CAST('01-01-20' AS DATE)
	
	UNION
	
	SELECT 1,
		'cancel',
		CAST('01-02-20' AS DATE)
	
	UNION
	
	SELECT 2,
		'start',
		CAST('01-03-20' AS DATE)
	
	UNION
	
	SELECT 2,
		'publish',
		CAST('01-04-20' AS DATE)
	
	UNION
	
	SELECT 3,
		'start',
		CAST('01-05-20' AS DATE)
	
	UNION
	
	SELECT 3,
		'cancel',
		CAST('01-06-20' AS DATE)
	
	UNION
	
	SELECT 4,
		'start',
		CAST('01-07-20' AS DATE)
	),
t1
AS (
	SELECT u1.user_id,
		SUM(CASE 
				WHEN u1.action = 'start'
					THEN 1
				ELSE 0
				END) AS starts,
		SUM(CASE 
				WHEN u1.action = 'cancel'
					THEN 1
				ELSE 0
				END) AS cancels,
		SUM(CASE 
				WHEN u1.action = 'publish'
					THEN 1
				ELSE 0
				END) AS publishes
	FROM users AS u1
	GROUP BY u1.user_id
	)
SELECT user_id,
	CAST(1.0 * publishes / starts AS DECIMAL(10, 1)) AS publish_rate,
	CAST(1.0 * cancels / starts AS DECIMAL(10, 1)) AS cancel_rate
FROM t1;

--task 2
WITH transactions (
	sender,
	receiver,
	amount,
	transaction_date
	)
AS (
	SELECT 5,
		2,
		10,
		CAST('2-12-20' AS DATE)
	
	UNION
	
	SELECT 1,
		3,
		15,
		CAST('2-13-20' AS DATE)
	
	UNION
	
	SELECT 2,
		1,
		20,
		CAST('2-13-20' AS DATE)
	
	UNION
	
	SELECT 2,
		3,
		25,
		CAST('2-14-20' AS DATE)
	
	UNION
	
	SELECT 3,
		1,
		20,
		CAST('2-15-20' AS DATE)
	
	UNION
	
	SELECT 3,
		2,
		15,
		CAST('2-15-20' AS DATE)
	
	UNION
	
	SELECT 1,
		4,
		5,
		CAST('2-16-20' AS DATE)
	),
debits
AS (
	SELECT sender,
		SUM(amount) AS debited
	FROM transactions
	GROUP BY sender
	),
credits
AS (
	SELECT receiver,
		SUM(amount) AS credited
	FROM transactions
	GROUP BY receiver
	)
SELECT COALESCE(sender, receiver) AS users,
	COALESCE(credited, 0) - COALESCE(debited, 0) AS net_change
FROM debits d
FULL JOIN credits c ON d.sender = c.receiver
ORDER BY 2 DESC;

--task 3
WITH items (
	dates,
	item
	)
AS (
	SELECT CAST('01-01-20' AS DATE),
		'apple'
	
	UNION ALL
	
	SELECT CAST('01-01-20' AS DATE),
		'apple'
	
	UNION ALL
	
	SELECT CAST('01-01-20' AS DATE),
		'pear'
	
	UNION ALL
	
	SELECT CAST('01-01-20' AS DATE),
		'pear'
	
	UNION ALL
	
	SELECT CAST('01-02-20' AS DATE),
		'pear'
	
	UNION ALL
	
	SELECT CAST('01-02-20' AS DATE),
		'pear'
	
	UNION ALL
	
	SELECT CAST('01-02-20' AS DATE),
		'pear'
	
	UNION ALL
	
	SELECT CAST('01-02-20' AS DATE),
		'orange'
	),
table1
AS (
	SELECT dates,
		item,
		count(*) AS counts
	FROM items
	GROUP BY dates,
		item
	),
table2
AS (
	SELECT *,
		RANK() OVER (
			PARTITION BY dates ORDER BY counts DESC
			) AS date_rank
	FROM table1
	)
SELECT dates,
	item
FROM table2
WHERE date_rank = 1;

--task 4
WITH users (
	user_id,
	action,
	action_date
	)
AS (
	SELECT 1,
		'start',
		CAST('2-12-20' AS DATE)
	
	UNION ALL
	
	SELECT 1,
		'cancel',
		CAST('2-13-20' AS DATE)
	
	UNION ALL
	
	SELECT 2,
		'start',
		CAST('2-11-20' AS DATE)
	
	UNION ALL
	
	SELECT 2,
		'publish',
		CAST('2-14-20' AS DATE)
	
	UNION ALL
	
	SELECT 3,
		'start',
		CAST('2-15-20' AS DATE)
	
	UNION ALL
	
	SELECT 3,
		'cancel',
		CAST('2-15-20' AS DATE)
	
	UNION ALL
	
	SELECT 4,
		'start',
		CAST('2-18-20' AS DATE)
	
	UNION ALL
	
	SELECT 1,
		'publish',
		CAST('2-19-20' AS DATE)
	),
t1
AS (
	SELECT *,
		ROW_NUMBER() OVER (
			PARTITION BY user_id ORDER BY action_date DESC
			) AS date_rank
	FROM users
	),
latest
AS (
	SELECT *
	FROM t1
	WHERE date_rank = 1
	),
next_latest
AS (
	SELECT *
	FROM t1
	WHERE date_rank = 2
	)
SELECT l1.user_id,
	DATEDIFF(DAY, l2.action_date, l1.action_date) AS days_elapsed
FROM latest l1
LEFT JOIN next_latest l2 ON l1.user_id = l2.user_id
ORDER BY 1;

--task 5
WITH users (
	user_id,
	product_id,
	transaction_date
	)
AS (
	SELECT 1,
		101,
		CAST('2-12-20' AS DATE)
	
	UNION ALL
	
	SELECT 2,
		105,
		CAST('2-13-20' AS DATE)
	
	UNION ALL
	
	SELECT 1,
		111,
		CAST('2-14-20' AS DATE)
	
	UNION ALL
	
	SELECT 3,
		121,
		CAST('2-15-20' AS DATE)
	
	UNION ALL
	
	SELECT 1,
		101,
		CAST('2-16-20' AS DATE)
	
	UNION ALL
	
	SELECT 2,
		105,
		CAST('2-17-20' AS DATE)
	
	UNION ALL
	
	SELECT 4,
		101,
		CAST('2-16-20' AS DATE)
	
	UNION ALL
	
	SELECT 3,
		105,
		CAST('2-15-20' AS DATE)
	),
t1
AS (
	SELECT *,
		ROW_NUMBER() OVER (
			PARTITION BY user_id ORDER BY transaction_date
			) AS transaction_number
	FROM users
	),
t2
AS (
	SELECT user_id,
		transaction_date
	FROM t1
	WHERE transaction_number = 2
	),
t3
AS (
	SELECT DISTINCT user_id
	FROM users
	)
SELECT t3.user_id,
	transaction_date AS superuser_date
FROM t3
LEFT JOIN t2 ON t3.user_id = t2.user_id
ORDER BY 1;

--task 6
with friends (user_id, friend) as
(
	select 1, 2
	union all
	select 1, 3
	union all
	select 1, 4
	union all
	select 2, 1
	union all
	select 3, 1
	union all
	select 3, 4
	union all
	select 4, 1
	union all
	select 4, 3
), likes (user_id, page_likes) as
(
	select 1, 'A'
	union all
	select 1, 'B'
	union all
	select 1, 'C'
	union all
	select 2, 'A'
	union all
	select 3, 'B'
	union all
	select 3, 'C'
	union all
	select 4, 'B'
), t1 as 
(
	SELECT 
	l.user_id, 
	l.page_likes, 
	f.friend
	FROM likes l
	JOIN friends f
	ON l.user_id = f.user_id
), t2 AS (
SELECT 
   t1.user_id,
   t1.page_likes, 
   t1.friend, 
   l.page_likes AS friend_likes
FROM t1
LEFT JOIN likes l
ON t1.friend = l.user_id
AND t1.page_likes = l.page_likes )
select 
	distinct t2.friend, t2.page_likes 
from t2
where t2.friend_likes is null;

--task 7
WITH mobile (user_id, page_url) 
AS 
(
	select 1, 'A'
	union all
	select 2, 'B'
	union all
	select 3, 'C'
	union all
	select 4, 'A'
	union all
	select 9, 'B'
	union all
	select 2, 'C'
	union all
	select 10, 'B'
), web (user_id, page_url) 
AS 
(
	select 6, 'A'
	union all
	select 2, 'B'
	union all
	select 3, 'C'
	union all
	select 7, 'A'
	union all
	select 4, 'B'
	union all
	select 8, 'C'
	union all
	select 5, 'B'
), t1 AS (
SELECT DISTINCT 
   m.user_id AS mobile_user, 
   w.user_id AS web_user
FROM mobile m
FULL JOIN web w
ON m.user_id = w.user_id
)
select  
	AVG(case when t1.mobile_user is not null and t1.web_user is null then CAST(1 as float) else CAST(0 as float) end) as mobile_fraction,
	AVG(CASE WHEN web_user IS NOT NULL AND mobile_user IS NULL THEN CAST(1 as float) ELSE CAST(0 as float) END) AS web_fraction,
	AVG(CASE WHEN web_user IS NOT NULL AND mobile_user IS NOT NULL THEN CAST(1 as float) ELSE CAST(0 as float) END) AS both_fraction
from t1;

--task 8
WITH users (user_id, name, join_date) 
AS 
(
	select 1, 'Jon', CAST('2-14-20' AS date)
	union all
	select 2, 'Jane', CAST('2-14-20' AS date)
	union all
	select 3, 'Jill', CAST('2-15-20' AS date) 
	union all
	select 4, 'Josh', CAST('2-15-20' AS date) 
	union all
	select 5, 'Jean', CAST('2-16-20' AS date) 
	union all
	select 6, 'Justin', CAST('2-17-20' AS date)
	union all
	select 7, 'Jeremy', CAST('2-18-20' AS date)
), events (user_id, type, access_date) 
AS 
( 
	select 1, 'F1', CAST('3-1-20' AS date)
	union all
	select 2, 'F2', CAST('3-2-20' AS date)
	union all
	select 2, 'P', CAST('3-12-20' AS date)
	union all
	select 3, 'F2', CAST('3-15-20' AS date) 
	union all
	select 4, 'F2', CAST('3-15-20' AS date) 
	union all
	select 1, 'P', CAST('3-16-20' AS date) 
	union all
	select 3, 'P', CAST('3-22-20' AS date)
), t1 as
(
	select 
		user_id,
		type,
		access_date AS f2_date
	from events
	where type = 'F2'
), t2 as
(
	select 
		user_id,
		type,
		access_date AS premium_date
	from events
	where type = 'P'
), t3 AS (
SELECT DATEDIFF(day, u.join_date, t2.premium_date) AS upgrade_time
FROM users u
JOIN t1
ON u.user_id = t1.user_id
LEFT JOIN t2
ON u.user_id = t2.user_id
)
select 
	Round(AVG(case when t3.upgrade_time < 30 then CAST(1 as float) else CAST(0 as float) end), 2)
from t3;

--task 9
WITH friends (user1, user2) 
AS (
	select 1, 2
	union all
	select 1, 3
	union all
	select 1, 4
	union all
	select 2, 3
), t1 as 
(
	select 
		user1
	from friends
	union all
	select 
		user2
	from friends
)
select 
	t1.user1, count(*) as friends_count
from t1
group by t1.user1
order by friends_count desc, t1.user1 asc;

--task 10
WITH projects (task_id, start_date, end_date) 
AS 
(
	select 1, CAST('10-01-20' AS date), CAST('10-02-20' AS date) 
	union all
	select 2, CAST('10-02-20' AS date), CAST('10-03-20' AS date)
	union all
	select 3, CAST('10-03-20' AS date), CAST('10-04-20' AS date) 
	union all
	select 4, CAST('10-13-20' AS date), CAST('10-14-20' AS date) 
	union all
	select 5, CAST('10-14-20' AS date), CAST('10-15-20' AS date) 
	union all
	select 6, CAST('10-28-20' AS date), CAST('10-29-20' AS date) 
	union all
	select 7, CAST('10-30-20' AS date), CAST('10-31-20' AS date)
), t1 AS 
(
	SELECT start_date
	FROM projects
	WHERE start_date NOT IN (SELECT end_date FROM projects) 
),t2 AS 
(
	SELECT end_date
	FROM projects
	WHERE end_date NOT IN (SELECT start_date FROM projects) 
), t3 AS 
(
	SELECT 
	t1.start_date, 
	MIN(t2.end_date) AS end_date
	FROM t1, t2
	WHERE t1.start_date < end_date
	GROUP BY t1.start_date
)
select 
	t3.start_date,
	t3.end_date,
	DATEDIFF(day, t3.start_date, t3.end_date) as duration
from t3
order by duration, t3.start_date;

--task 11
WITH attendance (student_id, school_date, attendance)
AS 
(
	select 1, CAST('2020-04-03' AS date), 0
	union all
	select 2, CAST('2020-04-03' AS date), 1
	union all
	select 3, CAST('2020-04-03' AS date), 1 
	union all
	select 1, CAST('2020-04-04' AS date), 1 
	union all
	select 2, CAST('2020-04-04' AS date), 1 
	union all
	select 3, CAST('2020-04-04' AS date), 1 
	union all
	select 1, CAST('2020-04-05' AS date), 0 
	union all
	select 2, CAST('2020-04-05' AS date), 1 
	union all
	select 3, CAST('2020-04-05' AS date), 1 
	union all
	select 4, CAST('2020-04-05' AS date), 1
), students (student_id, school_id, grade_level, date_of_birth)
AS 
(
	select 1, 2, 5, CAST('2012-04-03' AS date)
	union all
	select 2, 1, 4, CAST('2013-04-04' AS date)
	union all
	select 3, 1, 3, CAST('2014-04-05' AS date) 
	union all
	select 4, 2, 4, CAST('2013-04-03' AS date)
)
select 
	ROUND(AVG(CAST(attendance as float)), 2) as birthday_attendance
from students s
join attendance a
on s.student_id = a.student_id and (DAY(s.date_of_birth) = DAY(a.school_date) and MONTH(s.date_of_birth) = MONTH(a.school_date));

--task 12
WITH hackers (hacker_id, name)
AS 
(
	select 1, 'John'
	union all
	select 2, 'Jane'
	union all
	select 3, 'Joe'
	union all
	select 4, 'Jim'
),
submissions (submission_id, hacker_id, challenge_id, score)
AS 
(
	select 101, 1, 1, 10
	union all
	select 102, 1, 1, 12
	union all
	select 103, 2, 1, 11
	union all
	select 104, 2, 1, 9
	union all
	select 105, 2, 2, 13
	union all
	select 106, 3, 1, 9
	union all
	select 107, 3, 2, 12
	union all
	select 108, 3, 2, 15
	union all
	select 109, 4, 1, 0
), t1 as
(
	select
		h.hacker_id,
		h.name,
		s.challenge_id,
		max(s.score) as max_score
	from hackers h
	join submissions s
	on h.hacker_id = s.hacker_id
	group by h.hacker_id, h.name, s.challenge_id
)
select 
	t1.hacker_id,
	t1.name,
	sum(t1.max_score) as total_score
from t1
group by t1.hacker_id, t1.name
having sum(t1.max_score) != 0
order by total_score desc, t1.hacker_id;

--task 13
WITH scores (id, score)
AS 
(
	select 1, 3.50
	union all
	select 2, 3.65
	union all
	select 3, 4.00
	union all
	select 4, 3.85
	union all
	select 5, 4.00
	union all
	select 6, 3.65
)
SELECT score,
		DENSE_RANK() OVER (
			ORDER BY score DESC
			) AS date_rank
FROM scores;

--task 14
WITH employee (id, pay_month, salary)
AS 
(
	select 1, 1, 20
	union all
	select 2, 1, 20
	union all
	select 1, 2, 30
	union all
	select 2, 2, 30
	union all
	select 3, 2, 40
	union all
	select 1, 3, 40
	union all
	select 3, 3, 60
	union all
	select 1, 4, 60
	union all
	select 3, 4, 70
), t1 AS 
(
	SELECT *, 
		RANK() OVER (PARTITION BY id ORDER BY pay_month DESC) AS month_rank
	FROM employee
)
SELECT 
   id, 
   pay_month, 
   salary, 
   SUM(salary) OVER (PARTITION BY id ORDER BY month_rank DESC) AS cumulative_sum
FROM t1 
WHERE month_rank != 1
AND month_rank <= 4
ORDER BY 1, 2;

--task 15
WITH teams (team_id, team_name)
AS 
(
	select 1, 'New York'
	union all
	select 2, 'Atlanta'
	union all
	select 3, 'Chicago'
	union all
	select 4, 'Toronto'
	union all
	select 5, 'Los Angeles'
	union all
	select 6, 'Seattle'
), matches (match_id, host_team, guest_team, host_goals, guest_goals)
AS
(
	select 1, 1, 2, 3, 0
	union all
	select 2, 2, 3, 2, 4
	union all
	select 3, 3, 4, 4, 3
	union all
	select 4, 4, 5, 1, 1
	union all
	select 5, 5, 6, 2, 1
	union all
	select 6, 6, 1, 1, 2
), t1 AS (
SELECT 
   *, 
   CASE WHEN host_goals > guest_goals THEN 3 
        WHEN host_goals = guest_goals THEN 1 
        ELSE 0 END AS host_points, 
   CASE WHEN host_goals < guest_goals THEN 3 
   WHEN host_goals = guest_goals THEN 1 
   ELSE 0 END AS guest_points
FROM matches )
SELECT 
   t.team_name, 
   a.host_points + b.guest_points AS total_points
FROM teams t
JOIN t1 a
ON t.team_id = a.host_team
JOIN t1 b
ON t.team_id = b.guest_team
ORDER BY 2 DESC, 1;

--task 16
WITH customers (id, name)
AS (
	select 1, 'Daniel'
	union all
	select 2, 'Diana'
	union all
	select 3, 'Elizabeth'
	union all
	select 4, 'John'
),
orders (order_id, customer_id, product_name)
AS 
(
	select 1, 1, 'A'
	union all
	select 2, 1, 'B'
	union all
	select 3, 2, 'A'
	union all
	select 4, 2, 'B'
	union all
	select 5, 2, 'C'
	union all
	select 6, 3, 'A' 
	union all
	select 7, 3, 'A'
	union all
	select 8, 3, 'B'
	union all
	select 9, 3, 'D'
) 
SELECT DISTINCT 
   id, 
   name
FROM orders o
JOIN customers c
ON o.customer_id = c.id
WHERE customer_id IN (SELECT customer_id 
                      FROM orders 
                      WHERE product_name = 'A') 
AND customer_id IN (SELECT customer_id 
                    FROM orders 
                    WHERE product_name = 'B') 
AND customer_id NOT IN (SELECT customer_id 
                        FROM orders 
                        WHERE product_name = 'C')
ORDER BY 1;

--task 17
WITH stations (id, city, state, latitude, longitude)
AS 
(
	select 1, 'Asheville', 'North Carolina', 35.6, 82.6
	union all
	select 2, 'Burlington', 'North Carolina', 36.1, 79.4
	union all
	select 3, 'Chapel Hill', 'North Carolina', 35.9, 79.1
	union all
	select 4, 'Davidson', 'North Carolina', 35.5, 80.8
	union all
	select 5, 'Elizabeth City', 'North Carolina', 36.3, 76.3
	union all
	select 6, 'Fargo', 'North Dakota', 46.9, 96.8
	union all
	select 7, 'Grand Forks', 'North Dakota', 47.9, 97.0
	union all
	select 8, 'Hettinger', 'North Dakota', 46.0, 102.6
	union all
	select 9, 'Inkster', 'North Dakota', 48.2, 97.6
), t1 AS (
SELECT 
   *, 
   ROW_NUMBER() OVER (PARTITION BY state ORDER BY latitude ASC) AS row_number_state, 
            count(*) OVER (PARTITION BY state) AS row_count
FROM stations )
SELECT 
   state, 
   cast(AVG(latitude) as decimal(10, 1) ) AS median_latitude
FROM t1
WHERE row_number_state >= 1.0*row_count/2 
AND row_number_state <= 1.0*row_count/2 + 1
GROUP BY state;

--task 18
WITH stations (id, city, state, latitude, longitude)
AS 
(
	select 1, 'Asheville', 'North Carolina', 35.6, 82.6
	union all
	select 2, 'Burlington', 'North Carolina', 36.1, 79.4
	union all
	select 3, 'Chapel Hill', 'North Carolina', 35.9, 79.1
	union all
	select 4, 'Davidson', 'North Carolina', 35.5, 80.8
	union all
	select 5, 'Elizabeth City', 'North Carolina', 36.3, 76.3
	union all
	select 6, 'Fargo', 'North Dakota', 46.9, 96.8
	union all
	select 7, 'Grand Forks', 'North Dakota', 47.9, 97.0
	union all
	select 8, 'Hettinger', 'North Dakota', 46.0, 102.6
	union all
	select 9, 'Inkster', 'North Dakota', 48.2, 97.6
),
t1 AS (
SELECT 
   s1.state, 
   s1.city AS city1, 
   s2.city AS city2, 
   s1.latitude AS city1_lat, 
   s1.longitude AS city1_long, 
   s2.latitude AS city2_lat, 
   s2.longitude AS city2_long
FROM stations s1
JOIN stations s2
ON s1.state = s2.state 
AND s1.city < s2.city ),
t2 AS (
	SELECT *, 
	round(power((Power((cast(city1_lat as float) - cast(city2_lat as float)), 2) + power((cast(city1_long as float)- cast(city2_long as float)), 2)), 0.5), 2) AS distance
	FROM t1 ),
t3 AS (
	SELECT *, RANK() OVER (PARTITION BY state ORDER BY distance DESC) AS dist_rank
	FROM t2 
)
SELECT 
   state, 
   city1, 
   city2, 
   distance
FROM t3
WHERE dist_rank = 1;

--task 19
WITH users (user_id, join_date, invited_by) 
AS 
(
	select 1, CAST('01-01-20' AS date), 0
	union all
	select 2, CAST('01-10-20' AS date), 1
	union all
	select 3, CAST('02-05-20' AS date), 2
	union all
	select 4, CAST('02-12-20' AS date), 3
	union all
	select 5, CAST('02-25-20' AS date), 2
	union all
	select 6, CAST('03-01-20' AS date), 0
	union all
	select 7, CAST('03-01-20' AS date), 4
	union all
	select 8, CAST('03-04-20' AS date), 7
),
t1 AS (
SELECT 
   MONTH(u2.join_date) AS month,
   cast(datediff(day, u2.join_date, u1.join_date) as decimal(10, 2)) AS cycle_time
FROM users u1
JOIN users u2
ON u1.invited_by = u2.user_id )
SELECT 
   month, 
   cast(AVG(cycle_time) as decimal(10, 1)) AS cycle_time_month_avg
FROM t1
GROUP BY month
ORDER BY month;

--task 20
WITH attendance (event_date, visitors) 
AS 
( 
	select CAST('01-01-20' AS date), 10
	union all
	select CAST('01-04-20' AS date), 109
	union all
	select CAST('01-05-20' AS date), 150
	union all
	select CAST('01-06-20' AS date), 99
	union all
	select CAST('01-07-20' AS date), 145
	union all
	select CAST('01-08-20' AS date), 1455
	union all
	select CAST('01-11-20' AS date), 199
	union all
	select CAST('01-12-20' AS date), 188
),
t1 AS (
SELECT *, 
   ROW_NUMBER() OVER (ORDER BY event_date) AS day_num
FROM attendance 
),
t2 AS (
SELECT *
FROM t1
WHERE visitors > 100 ),
t3 AS (
SELECT 
   a.day_num AS day1, 
   b.day_num AS day2, 
   c.day_num AS day3
FROM t2 a
JOIN t2 b
ON a.day_num = b.day_num - 1
JOIN t2 c
ON a.day_num = c.day_num - 2 )
SELECT 
   event_date, 
   visitors
FROM t1
WHERE day_num IN (SELECT day1 FROM t3)
   OR day_num IN (SELECT day2 FROM t3)
   OR day_num IN (SELECT day3 FROM t3);

--task 21
WITH orders (order_id, customer_id, product_id) 
AS 
(
	select 1, 1, 1
	union all
	select 1, 1, 2
	union all
	select 1, 1, 3
	union all
	select 2, 2, 1
	union all
	select 2, 2, 2
	union all
	select 2, 2, 4
	union all
	select 3, 1, 5
),
products (id, name) 
AS 
(
	select 1, 'A'
	union all
	select 2, 'B'
	union all
	select 3, 'C'
	union all
	select 4, 'D'
	union all
	select 5, 'E'
),
t1 AS (
SELECT 
   o1.product_id AS prod_1, 
   o2.product_id AS prod_2
FROM orders o1
JOIN orders o2
ON o1.order_id = o2.order_id
AND o1.product_id < o2.product_id ),
t2 AS (
SELECT CONCAT(p1.name, ' ', p2.name) AS product_pair
FROM t1
JOIN products p1
ON t1.prod_1 = p1.id
JOIN products p2
ON t1.prod_2 = p2.id )
SELECT t2.product_pair, 
   COUNT(*) AS purchase_freq
FROM t2
group by t2.product_pair
ORDER BY purchase_freq DESC
OFFSET 0 ROWS
FETCH NEXT 3 ROWS ONLY;

--task 22 - ?

--task 23
WITH salaries (month, salary) 
AS 
(
	select 1, 2000
	union all
	select 2, 3000
	union all
	select 3, 5000
	union all
	select 4, 4000
	union all
	select 5, 2000
	union all
	select 6, 1000
	union all
	select 7, 2000
	union all
	select 8, 4000
	union all
	select 9, 5000
)
SELECT 
   s1.month, 
   SUM(s2.salary) AS salary_3mos
FROM salaries s1
JOIN salaries s2
ON s1.month <= s2.month 
AND s1.month > s2.month - 3
GROUP BY s1.month
HAVING s1.month < 7
ORDER BY s1.month;

--task 24
WITH trips (trip_id, rider_id, driver_id, status, request_date)
AS 
(
	select 1, 1, 10, 'completed', CAST('2020-10-01' AS date)
	union all
	select 2, 2, 11, 'cancelled_by_driver', CAST('2020-10-01' AS date)
	union all
	select 3, 3, 12, 'completed', CAST('2020-10-01' AS date)
	union all
	select 4, 4, 10, 'cancelled_by_rider', CAST('2020-10-02' AS date)
	union all
	select 5, 1, 11, 'completed', CAST('2020-10-02' AS date)
	union all
	select 6, 2, 12, 'completed', CAST('2020-10-02' AS date)
	union all
	select 7, 3, 11, 'completed', CAST('2020-10-03' AS date)
),
users (user_id, banned, type)
AS 
(
	select 1, 'no', 'rider'
	union all
	select 2, 'yes', 'rider'
	union all
	select 3, 'no', 'rider'
	union all
	select 4, 'no', 'rider'
	union all
	select 10, 'no', 'driver'
	union all
	select 11, 'no', 'driver'
	union all
	select 12, 'no', 'driver'
)
SELECT 
   request_date, 
   1 - AVG(CASE WHEN status = 'completed' THEN cast(1 as float) ELSE cast(0 as float) END) AS cancel_rate
FROM trips
WHERE rider_id NOT IN (SELECT user_id 
                       FROM users
                       WHERE banned = 'yes' )
AND driver_id NOT IN (SELECT user_id 
                      FROM users
                      WHERE banned = 'yes' )
GROUP BY request_date
HAVING DAY(request_date) <= 2;

--task 25 - ?