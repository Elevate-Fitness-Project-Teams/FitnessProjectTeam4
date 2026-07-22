using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixWorkoutExerciseExerciseRelationshi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId1",
                table: "WorkoutExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercises_ExerciseId1",
                table: "WorkoutExercises");

            migrationBuilder.RenameColumn(
                name: "ExerciseId1",
                table: "WorkoutExercises",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_WorkoutId",
                table: "WorkoutExercises",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercises_WorkoutId",
                table: "WorkoutExercises");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "WorkoutExercises",
                newName: "ExerciseId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises",
                columns: new[] { "WorkoutId", "ExerciseId" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId1",
                table: "WorkoutExercises",
                column: "ExerciseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId1",
                table: "WorkoutExercises",
                column: "ExerciseId1",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
