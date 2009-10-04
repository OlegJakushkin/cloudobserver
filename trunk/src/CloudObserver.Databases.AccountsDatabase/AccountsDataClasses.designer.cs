﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudObserver.Databases
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="CloudObserverAccountsDatabase")]
	public partial class AccountsDataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertGroupMember(GroupMember instance);
    partial void UpdateGroupMember(GroupMember instance);
    partial void DeleteGroupMember(GroupMember instance);
    partial void InsertCamera(Camera instance);
    partial void UpdateCamera(Camera instance);
    partial void DeleteCamera(Camera instance);
    partial void InsertGroupCamera(GroupCamera instance);
    partial void UpdateGroupCamera(GroupCamera instance);
    partial void DeleteGroupCamera(GroupCamera instance);
    partial void InsertGroup(Group instance);
    partial void UpdateGroup(Group instance);
    partial void DeleteGroup(Group instance);
    #endregion
		
		public AccountsDataClassesDataContext() : 
				base("Data Source=.\\sqlexpress;Initial Catalog=CloudObserverAccountsDatabase;Integrated" +
						" Security=True;Pooling=False", mappingSource)
		{
			OnCreated();
		}
		
		public AccountsDataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AccountsDataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AccountsDataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AccountsDataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<GroupMember> GroupMembers
		{
			get
			{
				return this.GetTable<GroupMember>();
			}
		}
		
		public System.Data.Linq.Table<Camera> Cameras
		{
			get
			{
				return this.GetTable<Camera>();
			}
		}
		
		public System.Data.Linq.Table<GroupCamera> GroupCameras
		{
			get
			{
				return this.GetTable<GroupCamera>();
			}
		}
		
		public System.Data.Linq.Table<Group> Groups
		{
			get
			{
				return this.GetTable<Group>();
			}
		}
	}
	
	[Table(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserID;
		
		private string _Email;
		
		private string _Password;
		
		private string _Name;
		
		private string _Description;
		
		private string _IconPath;
		
		private System.DateTime _RegistrationDate;
		
		private EntitySet<GroupMember> _GroupMembers;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIDChanging(int value);
    partial void OnUserIDChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnIconPathChanging(string value);
    partial void OnIconPathChanged();
    partial void OnRegistrationDateChanging(System.DateTime value);
    partial void OnRegistrationDateChanged();
    #endregion
		
		public User()
		{
			this._GroupMembers = new EntitySet<GroupMember>(new Action<GroupMember>(this.attach_GroupMembers), new Action<GroupMember>(this.detach_GroupMembers));
			OnCreated();
		}
		
		[Column(Storage="_UserID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[Column(Storage="_Email", DbType="VarChar(32) NOT NULL", CanBeNull=false)]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[Column(Storage="_Password", DbType="VarChar(32) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="VarChar(32) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_Description", DbType="VarChar(2048) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_IconPath", DbType="VarChar(256) NOT NULL", CanBeNull=false)]
		public string IconPath
		{
			get
			{
				return this._IconPath;
			}
			set
			{
				if ((this._IconPath != value))
				{
					this.OnIconPathChanging(value);
					this.SendPropertyChanging();
					this._IconPath = value;
					this.SendPropertyChanged("IconPath");
					this.OnIconPathChanged();
				}
			}
		}
		
		[Column(Storage="_RegistrationDate", DbType="DateTime NOT NULL")]
		public System.DateTime RegistrationDate
		{
			get
			{
				return this._RegistrationDate;
			}
			set
			{
				if ((this._RegistrationDate != value))
				{
					this.OnRegistrationDateChanging(value);
					this.SendPropertyChanging();
					this._RegistrationDate = value;
					this.SendPropertyChanged("RegistrationDate");
					this.OnRegistrationDateChanged();
				}
			}
		}
		
		[Association(Name="User_GroupMember", Storage="_GroupMembers", ThisKey="UserID", OtherKey="UserID")]
		public EntitySet<GroupMember> GroupMembers
		{
			get
			{
				return this._GroupMembers;
			}
			set
			{
				this._GroupMembers.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_GroupMembers(GroupMember entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_GroupMembers(GroupMember entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
	
	[Table(Name="dbo.GroupMembers")]
	public partial class GroupMember : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _UserID;
		
		private int _GroupID;
		
		private int _Privileges;
		
		private EntityRef<User> _User;
		
		private EntityRef<Group> _Group;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnUserIDChanging(int value);
    partial void OnUserIDChanged();
    partial void OnGroupIDChanging(int value);
    partial void OnGroupIDChanged();
    partial void OnPrivilegesChanging(int value);
    partial void OnPrivilegesChanged();
    #endregion
		
		public GroupMember()
		{
			this._User = default(EntityRef<User>);
			this._Group = default(EntityRef<Group>);
			OnCreated();
		}
		
		[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_UserID", DbType="Int NOT NULL")]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[Column(Storage="_GroupID", DbType="Int NOT NULL")]
		public int GroupID
		{
			get
			{
				return this._GroupID;
			}
			set
			{
				if ((this._GroupID != value))
				{
					if (this._Group.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnGroupIDChanging(value);
					this.SendPropertyChanging();
					this._GroupID = value;
					this.SendPropertyChanged("GroupID");
					this.OnGroupIDChanged();
				}
			}
		}
		
		[Column(Name="[Privileges]", Storage="_Privileges", DbType="Int NOT NULL")]
		public int Privileges
		{
			get
			{
				return this._Privileges;
			}
			set
			{
				if ((this._Privileges != value))
				{
					this.OnPrivilegesChanging(value);
					this.SendPropertyChanging();
					this._Privileges = value;
					this.SendPropertyChanged("Privileges");
					this.OnPrivilegesChanged();
				}
			}
		}
		
		[Association(Name="User_GroupMember", Storage="_User", ThisKey="UserID", OtherKey="UserID", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.GroupMembers.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.GroupMembers.Add(this);
						this._UserID = value.UserID;
					}
					else
					{
						this._UserID = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		[Association(Name="Group_GroupMember", Storage="_Group", ThisKey="GroupID", OtherKey="GroupID", IsForeignKey=true)]
		public Group Group
		{
			get
			{
				return this._Group.Entity;
			}
			set
			{
				Group previousValue = this._Group.Entity;
				if (((previousValue != value) 
							|| (this._Group.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Group.Entity = null;
						previousValue.GroupMembers.Remove(this);
					}
					this._Group.Entity = value;
					if ((value != null))
					{
						value.GroupMembers.Add(this);
						this._GroupID = value.GroupID;
					}
					else
					{
						this._GroupID = default(int);
					}
					this.SendPropertyChanged("Group");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.Cameras")]
	public partial class Camera : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CameraID;
		
		private string _Path;
		
		private string _Name;
		
		private string _Description;
		
		private string _IconPath;
		
		private System.DateTime _RegistrationDate;
		
		private EntitySet<GroupCamera> _GroupCameras;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCameraIDChanging(int value);
    partial void OnCameraIDChanged();
    partial void OnPathChanging(string value);
    partial void OnPathChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnIconPathChanging(string value);
    partial void OnIconPathChanged();
    partial void OnRegistrationDateChanging(System.DateTime value);
    partial void OnRegistrationDateChanged();
    #endregion
		
		public Camera()
		{
			this._GroupCameras = new EntitySet<GroupCamera>(new Action<GroupCamera>(this.attach_GroupCameras), new Action<GroupCamera>(this.detach_GroupCameras));
			OnCreated();
		}
		
		[Column(Storage="_CameraID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int CameraID
		{
			get
			{
				return this._CameraID;
			}
			set
			{
				if ((this._CameraID != value))
				{
					this.OnCameraIDChanging(value);
					this.SendPropertyChanging();
					this._CameraID = value;
					this.SendPropertyChanged("CameraID");
					this.OnCameraIDChanged();
				}
			}
		}
		
		[Column(Storage="_Path", DbType="VarChar(256) NOT NULL", CanBeNull=false)]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				if ((this._Path != value))
				{
					this.OnPathChanging(value);
					this.SendPropertyChanging();
					this._Path = value;
					this.SendPropertyChanged("Path");
					this.OnPathChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="VarChar(32) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_Description", DbType="VarChar(2048) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_IconPath", DbType="VarChar(256) NOT NULL", CanBeNull=false)]
		public string IconPath
		{
			get
			{
				return this._IconPath;
			}
			set
			{
				if ((this._IconPath != value))
				{
					this.OnIconPathChanging(value);
					this.SendPropertyChanging();
					this._IconPath = value;
					this.SendPropertyChanged("IconPath");
					this.OnIconPathChanged();
				}
			}
		}
		
		[Column(Storage="_RegistrationDate", DbType="DateTime NOT NULL")]
		public System.DateTime RegistrationDate
		{
			get
			{
				return this._RegistrationDate;
			}
			set
			{
				if ((this._RegistrationDate != value))
				{
					this.OnRegistrationDateChanging(value);
					this.SendPropertyChanging();
					this._RegistrationDate = value;
					this.SendPropertyChanged("RegistrationDate");
					this.OnRegistrationDateChanged();
				}
			}
		}
		
		[Association(Name="Camera_GroupCamera", Storage="_GroupCameras", ThisKey="CameraID", OtherKey="CameraID")]
		public EntitySet<GroupCamera> GroupCameras
		{
			get
			{
				return this._GroupCameras;
			}
			set
			{
				this._GroupCameras.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_GroupCameras(GroupCamera entity)
		{
			this.SendPropertyChanging();
			entity.Camera = this;
		}
		
		private void detach_GroupCameras(GroupCamera entity)
		{
			this.SendPropertyChanging();
			entity.Camera = null;
		}
	}
	
	[Table(Name="dbo.GroupCameras")]
	public partial class GroupCamera : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _CameraID;
		
		private int _GroupID;
		
		private EntityRef<Camera> _Camera;
		
		private EntityRef<Group> _Group;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnCameraIDChanging(int value);
    partial void OnCameraIDChanged();
    partial void OnGroupIDChanging(int value);
    partial void OnGroupIDChanged();
    #endregion
		
		public GroupCamera()
		{
			this._Camera = default(EntityRef<Camera>);
			this._Group = default(EntityRef<Group>);
			OnCreated();
		}
		
		[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[Column(Storage="_CameraID", DbType="Int NOT NULL")]
		public int CameraID
		{
			get
			{
				return this._CameraID;
			}
			set
			{
				if ((this._CameraID != value))
				{
					if (this._Camera.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCameraIDChanging(value);
					this.SendPropertyChanging();
					this._CameraID = value;
					this.SendPropertyChanged("CameraID");
					this.OnCameraIDChanged();
				}
			}
		}
		
		[Column(Storage="_GroupID", DbType="Int NOT NULL")]
		public int GroupID
		{
			get
			{
				return this._GroupID;
			}
			set
			{
				if ((this._GroupID != value))
				{
					if (this._Group.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnGroupIDChanging(value);
					this.SendPropertyChanging();
					this._GroupID = value;
					this.SendPropertyChanged("GroupID");
					this.OnGroupIDChanged();
				}
			}
		}
		
		[Association(Name="Camera_GroupCamera", Storage="_Camera", ThisKey="CameraID", OtherKey="CameraID", IsForeignKey=true)]
		public Camera Camera
		{
			get
			{
				return this._Camera.Entity;
			}
			set
			{
				Camera previousValue = this._Camera.Entity;
				if (((previousValue != value) 
							|| (this._Camera.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Camera.Entity = null;
						previousValue.GroupCameras.Remove(this);
					}
					this._Camera.Entity = value;
					if ((value != null))
					{
						value.GroupCameras.Add(this);
						this._CameraID = value.CameraID;
					}
					else
					{
						this._CameraID = default(int);
					}
					this.SendPropertyChanged("Camera");
				}
			}
		}
		
		[Association(Name="Group_GroupCamera", Storage="_Group", ThisKey="GroupID", OtherKey="GroupID", IsForeignKey=true)]
		public Group Group
		{
			get
			{
				return this._Group.Entity;
			}
			set
			{
				Group previousValue = this._Group.Entity;
				if (((previousValue != value) 
							|| (this._Group.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Group.Entity = null;
						previousValue.GroupCameras.Remove(this);
					}
					this._Group.Entity = value;
					if ((value != null))
					{
						value.GroupCameras.Add(this);
						this._GroupID = value.GroupID;
					}
					else
					{
						this._GroupID = default(int);
					}
					this.SendPropertyChanged("Group");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.Groups")]
	public partial class Group : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _GroupID;
		
		private string _Name;
		
		private string _Description;
		
		private string _IconPath;
		
		private int _Privacy;
		
		private System.DateTime _RegistrationDate;
		
		private EntitySet<GroupMember> _GroupMembers;
		
		private EntitySet<GroupCamera> _GroupCameras;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnGroupIDChanging(int value);
    partial void OnGroupIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnIconPathChanging(string value);
    partial void OnIconPathChanged();
    partial void OnPrivacyChanging(int value);
    partial void OnPrivacyChanged();
    partial void OnRegistrationDateChanging(System.DateTime value);
    partial void OnRegistrationDateChanged();
    #endregion
		
		public Group()
		{
			this._GroupMembers = new EntitySet<GroupMember>(new Action<GroupMember>(this.attach_GroupMembers), new Action<GroupMember>(this.detach_GroupMembers));
			this._GroupCameras = new EntitySet<GroupCamera>(new Action<GroupCamera>(this.attach_GroupCameras), new Action<GroupCamera>(this.detach_GroupCameras));
			OnCreated();
		}
		
		[Column(Storage="_GroupID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int GroupID
		{
			get
			{
				return this._GroupID;
			}
			set
			{
				if ((this._GroupID != value))
				{
					this.OnGroupIDChanging(value);
					this.SendPropertyChanging();
					this._GroupID = value;
					this.SendPropertyChanged("GroupID");
					this.OnGroupIDChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="VarChar(32) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_Description", DbType="VarChar(2048) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_IconPath", DbType="VarChar(256) NOT NULL", CanBeNull=false)]
		public string IconPath
		{
			get
			{
				return this._IconPath;
			}
			set
			{
				if ((this._IconPath != value))
				{
					this.OnIconPathChanging(value);
					this.SendPropertyChanging();
					this._IconPath = value;
					this.SendPropertyChanged("IconPath");
					this.OnIconPathChanged();
				}
			}
		}
		
		[Column(Storage="_Privacy", DbType="Int NOT NULL")]
		public int Privacy
		{
			get
			{
				return this._Privacy;
			}
			set
			{
				if ((this._Privacy != value))
				{
					this.OnPrivacyChanging(value);
					this.SendPropertyChanging();
					this._Privacy = value;
					this.SendPropertyChanged("Privacy");
					this.OnPrivacyChanged();
				}
			}
		}
		
		[Column(Storage="_RegistrationDate", DbType="DateTime NOT NULL")]
		public System.DateTime RegistrationDate
		{
			get
			{
				return this._RegistrationDate;
			}
			set
			{
				if ((this._RegistrationDate != value))
				{
					this.OnRegistrationDateChanging(value);
					this.SendPropertyChanging();
					this._RegistrationDate = value;
					this.SendPropertyChanged("RegistrationDate");
					this.OnRegistrationDateChanged();
				}
			}
		}
		
		[Association(Name="Group_GroupMember", Storage="_GroupMembers", ThisKey="GroupID", OtherKey="GroupID")]
		public EntitySet<GroupMember> GroupMembers
		{
			get
			{
				return this._GroupMembers;
			}
			set
			{
				this._GroupMembers.Assign(value);
			}
		}
		
		[Association(Name="Group_GroupCamera", Storage="_GroupCameras", ThisKey="GroupID", OtherKey="GroupID")]
		public EntitySet<GroupCamera> GroupCameras
		{
			get
			{
				return this._GroupCameras;
			}
			set
			{
				this._GroupCameras.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_GroupMembers(GroupMember entity)
		{
			this.SendPropertyChanging();
			entity.Group = this;
		}
		
		private void detach_GroupMembers(GroupMember entity)
		{
			this.SendPropertyChanging();
			entity.Group = null;
		}
		
		private void attach_GroupCameras(GroupCamera entity)
		{
			this.SendPropertyChanging();
			entity.Group = this;
		}
		
		private void detach_GroupCameras(GroupCamera entity)
		{
			this.SendPropertyChanging();
			entity.Group = null;
		}
	}
}
#pragma warning restore 1591
