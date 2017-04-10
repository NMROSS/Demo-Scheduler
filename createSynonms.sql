--create global accessable schema
CREATE SYNONYM paper
FOR [VirtualChair].[SCMS\username].paper
CREATE SYNONYM LAB
FOR [VirtualChair].[SCMS\username].LAB
CREATE SYNONYM DEMONSTRATOR
FOR [VirtualChair].[SCMS\username].DEMONSTRATOR
CREATE SYNONYM DEMOS
FOR [VirtualChair].[SCMS\username].DEMOS
CREATE SYNONYM GRADE
FOR [VirtualChair].[SCMS\username].GRADE
CREATE SYNONYM enrolled
FOR [VirtualChair].[SCMS\username].enrolled
CREATE SYNONYM previouslyDemoed
FOR [VirtualChair].[SCMS\username].previouslyDemoed
CREATE SYNONYM preferredPaper
FOR [VirtualChair].[SCMS\username].preferredPaper