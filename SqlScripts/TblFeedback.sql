USE runtime
;

DROP TABLE IF EXISTS `Feedback`
;

CREATE TABLE runtime.Feedback(
	RecordId INT AUTO_INCREMENT NOT NULL,
    EmployeeId VARCHAR(15),
    FeedbackDate DATETIME NOT NULL DEFAULT NOW(),
    Department VARCHAR(60) NOT NULL,
    FeedbackText1 TEXT,
    FeedbackRating1 INT NOT NULL,
    FeedbackText2 TEXT,
    FeedbackRating2 INT NOT NULL,
    FeedbackText3 TEXT,
    FeedbackRating3 INT NOT NULL,
    FeedbackText4 TEXT,    
    FeedbackRating4 INT NOT NULL,
    CONSTRAINT Pk_Feedback PRIMARY KEY (RecordId),
    CONSTRAINT Fk_Feedback_Account FOREIGN KEY (EmployeeId) REFERENCES Account (EmployeeId)
		ON DELETE SET NULL,
	CONSTRAINT Ck_Feedback_FeedbackRating1 CHECK (FeedbackRating1 >= 0 AND FeedbackRating <= 5),
    CONSTRAINT Ck_Feedback_FeedbackRating2 CHECK (FeedbackRating2 >= 0 AND FeedbackRating <= 5),
    CONSTRAINT Ck_Feedback_FeedbackRating3 CHECK (FeedbackRating3 >= 0 AND FeedbackRating <= 5),
    CONSTRAINT Ck_Feedback_FeedbackRating4 CHECK (FeedbackRating4 >= 0 AND FeedbackRating <= 5),
	CONSTRAINT Ck_Feedback_Department CHECK (Department IN (
		'Sales', 'HR', 'IT', 'Marketing', 'Accounting/Finance', 'Customer Service', 'Operations', 'Distribution'))
)
;

INSERT INTO runtime.Feedback(EmployeeId, Department, FeedbackText1, FeedbackRating1, FeedbackText2, FeedbackRating2, FeedbackText3, FeedbackRating3, FeedbackText4, FeedbackRating4)
	VALUES
		('1000', 'Sales', 'This is example feedback for question 1.  This is negative feedback.', 2, 'This is example feedback for question 2.  This is average feedback.', 3, 'This is example feedback for question 3.  This is positive feedback.', 5, 'This is example feedback for question 4.  This is positive feedback.', 4),
		('1001', 'Human Resources', 'This is example feedback for question 1.  This is average feedback.', 3, 'This is example feedback for question 2.  This is negative feedback.', 1, 'This is example feedback for question 3.  This is negative feedback.', 2, 'This is example feedback for question 4.  This is average feedback.', 3),
        ('1003', 'IT', 'This is example feedback for question 1.  This is positive feedback.', 5, 'This is example feedback for question 2.  This is positive feedback.', 4, 'This is example feedback for question 3.  This is average feedback.', 3, 'This is example feedback for question 4.  This is negative feedback.', 1),
		('1000', 'Marketing', 'This is example feedback for question 1.  This is negative feedback.', 2, 'This is example feedback for question 2.  This is average feedback.', 3, 'This is example feedback for question 3.  This is positive feedback.', 4, 'This is example feedback for question 4.  This is average feedback.', 3),
		('1001', 'Accounting/Finance', 'This is example feedback for question 1.  This is negative feedback.', 1, 'This is example feedback for question 2.  This is average feedback.', 3, 'This is example feedback for question 3.  This is positive feedback.', 4, 'This is example feedback for question 4.  This is average feedback.', 3),
		('1003', 'Customer Service', 'This is example feedback for question 1.  This is negative feedback.', 1, 'This is example feedback for question 2.  This is average feedback.', 3, 'This is example feedback for question 3.  This is positive feedback.', 4, 'This is example feedback for question 4.  This is negative feedback.', 2),
		('1000', 'Operations', 'This is example feedback for question 1.  This is positive feedback.', 5, 'This is example feedback for question 2.  This is negative feedback.', 2, 'This is example feedback for question 3.  This is positive feedback.', 4, 'This is example feedback for question 4.  This is positive feedback.', 5),
		('1001', 'Distribution', 'This is example feedback for question 1.  This is positive feedback.', 4, 'This is example feedback for question 2.  This is average feedback.', 3, 'This is example feedback for question 3.  This is average feedback.', 3, 'This is example feedback for question 4.  This is negative feedback.', 1)
;
INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('1003', 'Sales','I feel satisfy with my job, since my boss is a good leader', '5','If the sales department employ more staff, it would enhance the efficiency','4','Customers has many request, but the company did not provide sufficient support','3','I feel great for sales department','4');


INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('1001', 'IT','I am not happy with my job, the working time is too long', '2','If the tasks of each week is welly distributed,it will be great','3','The computers are old and slow','2','I feel the IT department is great, but it needs more people','3');



INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('1000', 'Accounting','My job is great, colleagues help each other, very good working environment', '5','If the company gives more time for the assigned task,it would be great','3','The calculators are old,the buttons are not responsive','2','Overall, the accounting department is great','4');




INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('1907', 'HR','The work-life balance of my job is great, I love it', '5','If the company can provide more rules and instructions, it will help us to make decisions','3','There are too much workplace politics, which can distract work performance','1','My work and department are not too bad','3');




INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('8888', 'Marketing','I feel my job is kind of busy, but I think it is acceptable', '4','If the company can increase the budget, it would be great','3','Sometimes, we have conflict with other deparmtents,the company should deal with that','2','Marketing is a good department to work, I love the culture','5');




INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('3593', 'Customer Service','I feel my job is challenging, and I feel greatful that I can help people', '5','The company should hire more people, we cannot handle the calls during peak hours','2','The headsets is not good at sound isolation, which affect the call quality','2','Cutomer Service deparment is awesome, I like it','5');







INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('1515', 'HR','Working at HR department is bad, the manager do not compassionate fellows situation', '1','The company should give more training to us, sometimes I do not know what to do in some situation','3','The office is too cold','2','HR deparmtent is not great, I wish I can work at other deparment','1');




INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('3284', 'Accounting','I have to bring my work home couple times a week, which is bad', '2','If the company upgrede the audit system, it will help a lot','3','There are too much tasks due within a week, it decrease the work performance','2','I love my colleagues, but the accounting department is too rushed','2');




INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('9493', 'IT','My job is good, it gives me a lot of opportunity', '5','If the company can upgrade the size of the screens, it will help a lot','3','The allowed time to complete the assigned development is too short','2','IT department is great, I love it','5');


INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('9584', 'Sales','Sales deparment is mean, they reqired work overtime frequently', '2','If the company provides longer lucnh time would be great, we did not have enough rest','3','There are too much gossip in the workplace','2','Sales deparment is fine, but not great','3');


INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('4309', 'HR','HR is good place to work, but the instructions are not clear', '3','If manager assigns tasks efficiently, it will help us a lot','3','The team meetings are so frequent,sometimes, it is not necessary','3','HR deparment is fine','3');

INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('1515', 'Sales','I love my job a lot', '5','I wish we had free lunches.','4','Nothing gets in the way of my job.','5','Overall I am satisfied with my job and everything with it','5');





INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('8778', 'Marketing','I feel my job is great. And my colleagues are very supportive', '5','If the teams are smaller, it will be easier for us to communicate','3','Conference room is always occupied','3','Marketing deparment is good to me','5');


INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('3023', 'Sales','I am not happy with my job.Sometimes customers are rude', '2','If the company give more support to us, we will feel better','3','Nothing','3','Sales department is great','5');

INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('6987', 'IT','IT deparment has a lot of work, but we do not have time to finish it.', '3','Hire more people will help','2','Nothing','3','IT department is fabulous','5');


INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('4393', 'Customer Service','Customer service is great to know different people', '5','More training will help','3','Nothing','3','Customer Service department is great','5');


INSERT INTO Feedback (EmployeeId, Department,FeedbackText1, FeedbackRating1,FeedbackText2,FeedbackRating2,FeedbackText3,FeedbackRating3,FeedbackText4,FeedbackRating4)
VALUES ('3593', 'HR','HR deparment helps me learn a lot', '5','More activities within the department will create better team spirit','4','Nothing','3','HR department is awesome','5');











































SELECT * FROM runtime.Feedback
;
	
/*
************************************************************
EXAMPLE QUERIES
*************************************************************
-- For reading all recent feedback with most recent at the top)
SELECT * FROM runtime.Feedback
	ORDER BY FeedbackDate DESC
;

-- For reading all feedback for a specific department...
	-- where @ denotes a parameter/variable value
SELECT * FROM runtime.Feedback
	WHERE Department = 'Sales'@Department
;

-- For writing feedback after user input...
	-- where @ denotes a parameter/variable value and {}indicates data type (would omit '{}' and contents from actual statement).  SEE BELOW FOR EXAMPLE WITHOUT DATA TYPES.
-- NOTE: FeedbackDate will be provided by default in table definition.
INSERT INTO runtime.Feedback(EmployeeId, Department, FeedbackText1, FeedbackRating1, FeedbackText2, FeedbackRating2, FeedbackText3, FeedbackRating3, FeedbackText4, FeedbackRating4)
	VALUES
		(@EmployeeId{string}, @Department{string}, @FeedbackText1{string}, @FeedbackRating1{int}, @FeedbackText2{string}, @FeedbackRating2{int}, @FeedbackText3{string}, @FeedbackRating3{int}, @FeedbackText4{string}, @FeedbackRating4{int})
;

-- For writing feedback after user input... (SAME AS ABOVE BUT WITHOUT DATA TYPES - EASIER TO COPY IF DATA TYPES ARE KNOWN)
	-- where @ denotes a parameter/variable value
-- NOTE: FeedbackDate will be provided by default in table definition.
INSERT INTO runtime.Feedback(EmployeeId, Department, FeedbackText1, FeedbackRating1, FeedbackText2, FeedbackRating2, FeedbackText3, FeedbackRating3, FeedbackText4, FeedbackRating4)
	VALUES
		(@EmployeeId, @Department, @FeedbackText1, @FeedbackRating1, @FeedbackText2, @FeedbackRating2, @FeedbackText3, @FeedbackRating3, @FeedbackText4, @FeedbackRating4)
;
*/



