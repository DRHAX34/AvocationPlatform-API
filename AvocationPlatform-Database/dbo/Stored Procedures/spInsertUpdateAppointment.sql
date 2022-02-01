CREATE PROCEDURE [dbo].[spInsertUpdateAppointment]
	@AppointmentId UNIQUEIDENTIFIER = NULL,
	@CandidateId UNIQUEIDENTIFIER,
	@RecruiterId UNIQUEIDENTIFIER,
	@RoomId UNIQUEIDENTIFIER = NULL,
	@StartTime DATETIME = NULL,
	@EndTime DATETIME = NULL,
	@OpeningId UNIQUEIDENTIFIER = NULL,
	@Stage INT = 0,
	@SysStatus VARCHAR(5) = 'O',
	@Username VARCHAR(150) = NULL
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	IF(EXISTS(SELECT * FROM [dbo].[Appointments] WHERE [Id] = @AppointmentId))
	BEGIN
		UPDATE [dbo].[Appointments] SET [RoomId] = @RoomId,
			[StartTime] = @StartTime,
			[EndTime] = @EndTime,
			[OpeningId] = @OpeningId,
			[Stage] = @Stage,
			[SYS_STATUS] = @SysStatus,
			[SYS_MODIFY_DATE] = @CurrentDate,
			[SYS_MODIFY_USER_ID] = @Username
			WHERE [Id] = @AppointmentId
	END
	ELSE
	BEGIN
		SET @AppointmentId = NEWID()

		INSERT INTO [Appointments]([Id]
			,[CandidateId]
			,[RecruiterId]
			,[RoomId]
			,[StartTime]
			,[EndTime]
			,[OpeningId]
			,[Stage]
			,[SYS_STATUS]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@AppointmentId
			,@CandidateId
			,@RecruiterId
			,@RoomId
			,@StartTime
			,@EndTime
			,@OpeningId
			,@Stage
			,@SysStatus
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @AppointmentId
	END
	ELSE
	BEGIN
		SELECT @AppointmentId = NULL
	END
END
