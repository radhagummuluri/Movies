select m.Title, m.YearOfRelease, m.RunningTime, mr.Rating as Rating
from dbo.MovieRatings mr
inner join Movies m on m.ID = mr.MovieID
inner join MovieGenres mg on m.MovieID = m.ID 
inner join Genres g on g.ID = mg.GenreID
where mr.UserID = 1
order by Rating desc, Title asc

select top 5 m.ID, m.Title, m.YearOfRelease, m.RunningTime, Avg(mr.Rating * 1.0) as Rating
from dbo.MovieRatings mr
inner join Movies m on m.ID = mr.MovieID
inner join MovieGenres mg on m.MovieID = m.ID 
inner join Genres g on g.ID = mg.GenreID 
group by m.ID, m.Title, m.YearOfRelease, m.RunningTime, m.Genres
order by Rating desc, Title asc


select m.Title, g.Name 
from [dbo].[Movies] m
inner join MovieGenres mg on mg.MovieID = m.ID 
inner join Genres g on g.ID = mg.GenreID
order by m.ID

select * from [dbo].[Movies]
select * from [dbo].[Genres]
select * from [dbo].[Users]
