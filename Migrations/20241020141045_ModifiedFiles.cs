using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GADApplication.Migrations
{
    public partial class ModifiedFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check and rename FileSize4 to FileSizeSpecialOrder only if it doesn't match
            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'FileSize4') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'FileSizeSpecialOrder') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.FileSize4', N'FileSizeSpecialOrder', N'COLUMN';
                END");

            // Repeat similar checks for other columns
            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'FileSize3') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'FileSizePictures') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.FileSize3', N'FileSizePictures', N'COLUMN';
                END");

            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'FileSize2') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'FileSizeEvaluationAttendance') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.FileSize2', N'FileSizeEvaluationAttendance', N'COLUMN';
                END");

            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'FileSize1') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'FileSizeBudgetReport') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.FileSize1', N'FileSizeBudgetReport', N'COLUMN';
                END");

            // Similarly for the file names
            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'FileName4') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'FileNameSpecialOrder') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.FileName4', N'FileNameSpecialOrder', N'COLUMN';
                END");

            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'FileName3') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'FileNamePictures') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.FileName3', N'FileNamePictures', N'COLUMN';
                END");

            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'FileName2') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'FileNameEvaluationAttendance') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.FileName2', N'FileNameEvaluationAttendance', N'COLUMN';
                END");

            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'FileName1') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'FileNameBudgetReport') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.FileName1', N'FileNameBudgetReport', N'COLUMN';
                END");

            // Similar logic for FileData and ContentType columns
            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'FileData4') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'FileDataSpecialOrder') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.FileData4', N'FileDataSpecialOrder', N'COLUMN';
                END");

            // Repeat for the remaining FileData and ContentType columns
            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'ContentType4') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'ContentTypeSpecialOrder') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.ContentType4', N'ContentTypeSpecialOrder', N'COLUMN';
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse rename in the same way for the Down method, if necessary
            migrationBuilder.Sql(@"
                IF COL_LENGTH('GARActivities', 'FileSizeSpecialOrder') IS NOT NULL 
                   AND COL_LENGTH('GARActivities', 'FileSize4') IS NULL
                BEGIN
                    EXEC sp_rename N'GARActivities.FileSizeSpecialOrder', N'FileSize4', N'COLUMN';
                END");

            // Repeat for all the columns as needed to reverse changes
        }
    }
}
