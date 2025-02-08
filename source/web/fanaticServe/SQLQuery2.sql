

select
le.*
,sl.live_event_id
,sl.set_list_no
,sl.title
,sln.note	
from
[dbo].[live_event] le
join set_list sl
on le.live_event_id = sl.live_event_id	
join set_list_note sln 
on sln.set_list_id = sl.set_list_id

order by sl.live_event_id, sl.set_list_no


