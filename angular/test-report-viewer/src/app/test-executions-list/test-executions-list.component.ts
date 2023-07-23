import { Apollo, gql } from 'apollo-angular';
import { Component, OnInit } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { Env } from 'src/.env';

const GET_EXECUTIONS = gql`
query GetExecution {
  test_executions_execution {
    id
    result
    execution_time
    test_name
    execution_time_stamp
  }
}
`;

@Component({
  selector: 'app-test-executions-list',
  templateUrl: './test-executions-list.component.html',
  styleUrls: ['./test-executions-list.component.scss']
})
export class TestExecutionsListComponent implements OnInit {
  loading = true;
  error: any;
  data?: any[];

  constructor(private apollo: Apollo) { }

  ngOnInit(): void {
    this.apollo
      .watchQuery({
        query: GET_EXECUTIONS,
        context: {
          headers: new HttpHeaders().set("x-hasura-admin-secret", Env.HasuraAdminSecret),
        }
      })
      .valueChanges.subscribe((result: any) => {
        this.data = result.data?.test_executions_execution;
        this.loading = result.loading;
        this.error = result.error;
      });
  }

}
