<script context="module">
  import { writable, readable, derived } from "svelte/store";

  //export const filter = writable({ search: null });
  //export const data = writable([]);
  //export const pager = writable({});

  function createState() {
    const { subscribe, set, update } = writable({});

    fetch(`/api/mols/filter`).then(async x => set(await x.json()))
    // const { subscribe, set, update } = writable(await q.json());

    return {
      subscribe,
      set,
      reset: () => {
        set(null)
      }
    };
  }

  export const filter = createState();
</script>

<script>
  import Layout from "../../Shared/Layout.svelte";
  import Pager from "../../Shared/Pager.svelte";
  import SortLink from "../../Shared/SortLink.svelte";
  import Filter from "./Filter.svelte";
  import { stringify } from "query-string";

  import { onMount, onDestroy } from "svelte";
  import { reglament } from "../../store.js";
  import moment from "moment";

  let apply = e => filter.set({ ..._filter, page: 1 });
  let sorted = e => ($filter.sort = e.detail);
  let sort = "NAME";

  let _filter = $filter,
    data = [],
    pager = {};

  const unsubscribe = filter.subscribe(async x => {
    let q = await fetch(`/api/mols?${stringify(x)}`);
    pager = JSON.parse(q.headers.get("pager"));
    data = await q.json();
  });
  onDestroy(unsubscribe);
</script>

<Layout filter={_filter} on:apply={apply}>
  <div class="container">

    <h5>Сотрудники.</h5>

    <table class="table table-striped table-sm">
      <thead>
        <tr>
          <th>#</th>
          <th />
          <th>
            <SortLink name="MOL_ID" {sort} on:sorted={sorted}>Код</SortLink>
          </th>
          <th>
            <SortLink name="NAME" {sort} on:sorted={sorted}>Имя</SortLink>
          </th>
          <th>
            <SortLink name="NAME_LOGON" {sort} on:sorted={sorted}>
              Логин
            </SortLink>
          </th>
          <th>
            <SortLink name="EMAIL" {sort} on:sorted={sorted}>Email</SortLink>
          </th>
          <th>
            <SortLink name="PHONE_LOCAL" {sort} on:sorted={sorted}>
              Тел.
            </SortLink>
          </th>
          <th>
            <SortLink name="DEPT_NAME" {sort} on:sorted={sorted}>
              Отдел
            </SortLink>
          </th>
          <th>
            <SortLink name="POST_NAME" {sort} on:sorted={sorted}>
              Пост
            </SortLink>
          </th>
          <th>
            <SortLink name="DATE_HIRE" {sort} on:sorted={sorted}>Дата</SortLink>
          </th>
        </tr>
      </thead>

      <tbody>
        {#each data as item, i}
          <tr>
            <td>{i + pager.offset + 1}</td>
            <td>
              <a href="/mols/{item.MOL_ID}">
                <i class="fa fa-id-card-o" />
              </a>
            </td>
            <td>{item.MOL_ID}</td>
            <td>{item.NAME}</td>
            <td>
              {#if item.NAME_LOGON}{item.NAME_LOGON}{/if}
            </td>
            <td>
              {#if item.EMAIL}{item.EMAIL}{/if}
            </td>
            <td>
              {#if item.PHONE_LOCAL}{item.PHONE_LOCAL}{/if}
            </td>
            <td>
              {#if item.DEPT_NAME}{item.DEPT_NAME}{/if}
            </td>
            <td>
              {#if item.POST_NAME}{item.POST_NAME}{/if}
            </td>
            <td>
              {#if item.DATE_HIRE}
                {moment(item.DATE_HIRE).format('DD.MM.YYYY')}
              {/if}
            </td>
          </tr>
        {/each}

      </tbody>
    </table>

    <div class="mb-3">
      <Pager {...pager} on:paged={e => ($filter.page = e.detail)} />
    </div>

  </div>

  <div slot="filter">

    <Filter filter={_filter} />
  </div>

  <div slot="actions">
    <a
      class="dropdown-item"
      href="/mols/new"
      class:disabled={!$reglament.allowEdit}>
      Новый сотрудник
    </a>
  </div>

</Layout>
