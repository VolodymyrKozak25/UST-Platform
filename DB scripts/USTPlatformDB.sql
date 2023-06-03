CREATE TABLE IF NOT EXISTS users(
	user_id SERIAL PRIMARY KEY,
	first_name VARCHAR(16) NOT NULL,
	middle_name VARCHAR(16) NOT NULL,
	last_name VARCHAR(16) NOT NULL,
	password TEXT NOT NULL CHECK (LENGTH(password) >= 8),
	email TEXT UNIQUE NOT NULL CHECK (email LIKE '%@%'),
	user_type text NOT NULL CHECK (user_type IN ('student', 'teacher', 'admin'))
);

CREATE TABLE IF NOT EXISTS teachers(
	user_id int PRIMARY KEY REFERENCES users (user_id)
);

CREATE TABLE IF NOT EXISTS groups(
	group_id SERIAL PRIMARY KEY,
	name VARCHAR(12) UNIQUE NOT NULL
);

CREATE TABLE IF NOT EXISTS courses(
	course_id SERIAL PRIMARY KEY,
	name VARCHAR(64) UNIQUE NOT NULL,
	course_key int UNIQUE NOT NULL,
	max_teachers int DEFAULT 1
);

CREATE TABLE IF NOT EXISTS students(
	user_id int PRIMARY KEY REFERENCES users (user_id),
	group_id int NOT NULL,
	
	CONSTRAINT group_student
		FOREIGN KEY (group_id)
			REFERENCES groups(group_id)
			ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS tests(
	test_id SERIAL PRIMARY KEY,
	course_id int NOT NULL,
	name VARCHAR(64) UNIQUE NOT NULL,
	time_limit interval NOT NULL,
	final_date TIMESTAMPTZ NOT NULL,
	
	CONSTRAINT course_test
		FOREIGN KEY (course_id)
			REFERENCES courses(course_id)
			ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS questions(
	question_id SERIAL PRIMARY KEY,
	test_id int NOT NULL,
	question_text TEXT NOT NULL,
	score int NOT NULL,
	
	CONSTRAINT test_question
		FOREIGN KEY (test_id)
			REFERENCES tests(test_id)
			ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS answers(
	answer_id SERIAL PRIMARY KEY,
	question_id int NOT NULL,
	answer_text TEXT NOT NULL,
	is_correct BOOLEAN DEFAULT FALSE,
	
	CONSTRAINT question_answer
		FOREIGN KEY (question_id)
			REFERENCES questions(question_id)
			ON DELETE CASCADE
);

-- CREATE TABLE IF NOT EXISTS results(
-- 	student_id int REFERENCES students (user_id) ON UPDATE CASCADE ON DELETE CASCADE,
--   	test_id int REFERENCES tests (test_id) ON UPDATE CASCADE ON DELETE CASCADE,
-- 	CONSTRAINT results_id PRIMARY KEY (student_id, test_id)
-- );

CREATE TABLE IF NOT EXISTS studentResults(
	student_id int REFERENCES students (user_id) ON UPDATE CASCADE ON DELETE CASCADE,
	test_id int REFERENCES tests (test_id) ON UPDATE CASCADE ON DELETE CASCADE,
  	answer_id int REFERENCES answers (answer_id) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT sresult_id PRIMARY KEY (student_id, test_id, answer_id)
);

CREATE TABLE IF NOT EXISTS enrollment(
	group_id int REFERENCES groups (group_id) ON UPDATE CASCADE ON DELETE CASCADE,
  	course_id int REFERENCES courses (course_id) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT enrollment_id PRIMARY KEY (group_id, course_id)
);


CREATE TABLE IF NOT EXISTS teacherCourses(
	teacher_id int REFERENCES teachers (user_id) ON UPDATE CASCADE ON DELETE CASCADE,
  	course_id int REFERENCES courses (course_id) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT tcourse_id PRIMARY KEY (teacher_id, course_id)
);

CREATE TABLE IF NOT EXISTS testAvailability(
	test_id int REFERENCES tests (test_id) ON UPDATE CASCADE ON DELETE CASCADE,
  	group_id int REFERENCES groups (group_id) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT test_availability_id PRIMARY KEY (test_id, group_id)
);


-- DROP TABLE enrollment;
-- DROP TABLE teachercourses;
-- DROP TABLE studentresults;
-- DROP TABLE results;
-- DROP TABLE teachers;
-- DROP TABLE students;
-- DROP TABLE users;
-- DROP TABLE answers;
-- DROP TABLE questions;
-- DROP TABLE tests;
-- DROP TABLE courses;
-- DROP TABLE groups;