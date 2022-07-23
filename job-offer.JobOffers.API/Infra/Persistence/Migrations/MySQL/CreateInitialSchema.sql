CREATE TABLE offerer (
  offerer_id VARCHAR(36) NOT NULL,
  cod_offerer VARCHAR(15) NOT NULL,
  is_active BIT NOT NULL,
  created_at_utc DATETIME NOT NULL,
  updated_at_utc DATETIME NOT NULL,
  PRIMARY KEY (offerer_id)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE employer (
  employer_id VARCHAR(36) NOT NULL,
  cod_employer VARCHAR(15) NOT NULL,
  is_active BIT NOT NULL,
  created_at_utc DATETIME NOT NULL,
  updated_at_utc DATETIME NOT NULL,
  PRIMARY KEY (employer_id)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE offer_job (
  offer_job_id VARCHAR(36) NOT NULL,
  cod_oferta VARCHAR(15) NOT NULL,
  description VARCHAR(100)  NULL,
  tittle VARCHAR(50) NOT NULL,
  date_start_at_utc DATETIME  NULL,
  date_end_at_utc DATETIME  NULL,
  offer_job_state_id TINYINT(4) UNSIGNED NOT NULL,
  employer_id VARCHAR(36) NOT NULL,
  age_min INT NULL,
  age_max INT NULL,
  mto_remuneration FLOAT NULL,
  currency_remuneration varchar(10) NULL,
  total_vacancies INT NULL,
  num_vacancies INT NULL,
  time_experience INT NULL,
  is_active BIT NOT NULL,
  created_at_utc DATETIME NOT NULL,
  updated_at_utc DATETIME NOT NULL,
  PRIMARY KEY (offer_job_id),
  CONSTRAINT FK_offer_job_employer_id FOREIGN KEY(employer_id) REFERENCES employer(employer_id)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE postulation (
  postulation_id VARCHAR(36) NOT NULL,
  offer_job_id VARCHAR(36) NOT NULL,
  offerer_id VARCHAR(36) NOT NULL,
  postulation_state_id TINYINT(4) UNSIGNED NOT NULL,
  flag_experience BIT  NULL,
  flag_academic BIT  NULL,
  flag_courses BIT  NULL,
  flag_age BIT  NULL,
  created_at_utc DATETIME NOT NULL,
  updated_at_utc DATETIME NOT NULL,
  PRIMARY KEY(postulation_id),
  CONSTRAINT FK_postulation_offerer_id FOREIGN KEY(offerer_id) REFERENCES offerer(offerer_id),
  CONSTRAINT FK_postulation_offer_job_id FOREIGN KEY(offer_job_id) REFERENCES offer_job(offer_job_id)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE outbox(
  outbox_id BIGINT(20) NOT NULL AUTO_INCREMENT,
  message_id VARCHAR(255) NOT NULL,
  dispatched TINYINT(1) NOT NULL,
  dispatched_at DATETIME DEFAULT NULL,
  transport_operations TEXT DEFAULT NULL,
  PRIMARY KEY (outbox_id),
  UNIQUE INDEX UQ_outbox_message_id(message_id),
  INDEX IX_outbox_dispatched(dispatched),
  INDEX IX_outbox_dispatched_at(dispatched_at)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE PostulationSagaData (
  Id VARCHAR(40) NOT NULL,
  Originator VARCHAR(255) DEFAULT NULL,
  OriginalMessageId VARCHAR(255) DEFAULT NULL,
  PostulationId VARCHAR(255) DEFAULT NULL,
  JobOfferId VARCHAR(255) DEFAULT NULL,
  OffererId VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (Id),
  UNIQUE INDEX UQ_perform_postulation_saga_data_PostulationId(PostulationId)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
;