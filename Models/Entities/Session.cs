using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;

namespace PostOffice.Models.Entities;
//Класс, который описывает состояния сессии пользователя.
[EntityTypeConfiguration(typeof(SessionConfiguration))]
public class Session
{
	public int Id { get; set; }

	public Guid SessionId { get; set; }

	//Идентификатор учётной записи пользователя, хранимый в сессии.
	public int UserAccountId { get; set; }
	
	//Ссылка на учётную запись пользователя, хранимая в сессии (Навигационное свойство).
	public virtual UserAccount UserAccount { get; set; }

	//Дата начала сессии.
	public DateTime CreatedDate { get; set; }

	//Дата истечения срока действия сессии.
	public DateTime ExpiredDate { get; set; }
}
