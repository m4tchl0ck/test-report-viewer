SET check_function_bodies = false;
CREATE SCHEMA test_executions;
CREATE TABLE test_executions.execution (
    id bigint NOT NULL,
    test_name text NOT NULL,
    result character varying(4) NOT NULL,
    execution_time interval NOT NULL,
    execution_time_stamp timestamp with time zone NOT NULL
);
CREATE SEQUENCE test_executions.execution_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
ALTER SEQUENCE test_executions.execution_id_seq OWNED BY test_executions.execution.id;
ALTER TABLE ONLY test_executions.execution ALTER COLUMN id SET DEFAULT nextval('test_executions.execution_id_seq'::regclass);
ALTER TABLE ONLY test_executions.execution
    ADD CONSTRAINT execution_pkey PRIMARY KEY (id);
