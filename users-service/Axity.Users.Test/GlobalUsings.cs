global using System;
global using System.Collections.Generic;
global using System.Data.Common;
global using System.Linq;
global using System.Threading.Tasks;
global using AutoMapper;
global using Axity.Commons.Exceptions;
global using Axity.Users.Common.DTOs.Requests.Users;
global using Axity.Users.Common.DTOs.Responses.Users;
global using Axity.Users.Facade.Users;
global using Axity.Users.Facade.Users.Impl;
global using Axity.Users.Model.Entities;
global using Axity.Users.Persistence.Context;
global using Axity.Users.Persistence.DAO.Users;
global using Axity.Users.Persistence.DAO.Users.Impl;
global using Axity.Users.Services.Mapping;
global using Axity.Users.Services.Users.Impl;
global using Axity.Users.Services.Users;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.Data.Sqlite;
global using Microsoft.EntityFrameworkCore;
global using Moq;
global using NUnit.Framework;
global using NUnit.Framework.Legacy;