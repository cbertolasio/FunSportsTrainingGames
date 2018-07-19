FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

#RUN install the unixodbc, redshift drivers, and other utilities
RUN apt-get update && apt-get install -y --no-install-recommends apt-utils gnupg wget unixodbc
RUN wget https://s3.amazonaws.com/redshift-downloads/drivers/odbc/1.4.1.1001/AmazonRedshiftODBC-64-bit-1.4.1.1001-1.x86_64.deb
RUN apt install ./AmazonRedshiftODBC-64-bit-1.4.1.1001-1.x86_64.deb

COPY Docker/odbcinst.ini /etc/odbcinst.ini
COPY Docker/amazon.redshiftodbc.ini /opt/amazon/redshiftodbc/lib/64/amazon.redshiftodbc.ini

#RUN setup the enviroment
RUN export LD_LIBRARY_PATH=$LD_LIBRARY_PATH:/usr/local/lib
RUN export ODBCINI=/etc/odbc.ini
RUN export AMAZONREDSHIFTODBCINI=/opt/amazon/redshiftodbc/lib/64/amazon.redshiftodbc.ini
RUN export ODBCSYSINI=/etc

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src

COPY IrrigationReportingWebApi/IrrigationReportingWebApi.csproj IrrigationReportingWebApi/
COPY DependencyInjection/DependencyInjection.csproj DependencyInjection/
COPY BusinessLogic/BusinessLogic.csproj BusinessLogic/
COPY BusinessContracts/BusinessContracts.csproj BusinessContracts/
COPY DataContracts/DataContracts.csproj DataContracts/
COPY DataAccess/DataAccess.csproj DataAccess/
COPY DataModels/DataModels.csproj DataModels/
RUN dotnet restore IrrigationReportingWebApi/IrrigationReportingWebApi.csproj

COPY . .
WORKDIR /src/IrrigationReportingWebApi
RUN pwd
RUN dotnet build IrrigationReportingWebApi.csproj -c Release -o /app

WORKDIR /src/IrrigationReportingWebApi
FROM build AS publish
RUN dotnet publish IrrigationReportingWebApi.csproj -c Release -o /app


FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IrrigationReportingWebApi.dll"]
