select m.Title, m.YearOfRelease, m.RunningTime, mr.Rating as Rating
from dbo.MovieRatings mr
inner join Movies m on m.ID = mr.MovieID
where mr.UserID = 1
order by Rating desc, Title asc

select top 5 m.ID, m.Title, m.YearOfRelease, m.RunningTime, Avg(mr.Rating * 1.0) as Rating
from dbo.MovieRatings mr
inner join dbo.Movies m on m.ID = mr.MovieID
inner join dbo.MovieGenres mg on mg.MovieID = m.ID 
inner join dbo.Genres g on g.ID = mg.GenreID 
group by m.ID, m.Title, m.YearOfRelease, m.RunningTime
order by Rating desc, Title asc

select m.ID, m.Title, g.Name 
from [dbo].[Movies] m
inner join MovieGenres mg on mg.MovieID = m.ID 
inner join Genres g on g.ID = mg.GenreID
order by m.ID

select * from [dbo].[Movies]
select * from [dbo].[Genres]
select * from [dbo].[Users]

-------------------------------
DECLARE @title VARCHAR(100) = ''
DECLARE @year INT = 0
DECLARE @genreList TABLE (genre varchar(100))
INSERT INTO @genreList(genre) VALUES ('Comedy')
INSERT INTO @genreList(genre) VALUES ('Drama')

DECLARE @genreFilterCount INT = 0;
SELECT @genreFilterCount = COUNT(1) from @genreList

DECLARE @filteredListCount INT = 0
DECLARE @filteredList TABLE (ID INT, genCt INT)

Insert into @filteredList(ID, genCt)
select m.ID , count(mg.MovieID)
from [dbo].[Movies] m
inner join [dbo].[MovieGenres] mg on mg.MovieID = m.ID
inner join [dbo].[Genres] g on g.ID = mg.GenreID
where g.Name in (select genre from @genreList)
group by m.ID
having count(mg.MovieID) = 2

SELECT @filteredListCount = Count(1) from @filteredList

--SELECT * from @filteredList

select m.ID, m.Title, m.YearOfRelease, m.RunningTime, Avg(mr.Rating * 1.0) as Rating
from dbo.MovieRatings mr
inner join dbo.Movies m on m.ID = mr.MovieID
inner join dbo.MovieGenres mg on mg.MovieID = m.ID 
inner join dbo.Genres g on g.ID = mg.GenreID 
where (
(@title = '' or (@title != '' and m.Title like '%'+@title+'%'))
and ((@year = 0 or (@year >0 and m.YearOfRelease = @year)))
and 
(@genreFilterCount = 0 or (@genreFilterCount > 0 and @filteredListCount > 0 and m.ID in (select ID from @filteredList)))
)
group by m.ID, m.Title, m.YearOfRelease, m.RunningTime
order by Rating desc, Title asc