<script context="module">
  import { writable } from "svelte/store";
  export const filter = writable({});

</script>

<script>
  import Layout from "../../Shared/Layout.svelte";
  import Pager from "../../Shared/Pager.svelte";
  import Filter from "./Filter.svelte";
  import { stringify } from "query-string";

  import { onMount, onDestroy } from "svelte";
  import { reglament } from "../../store.js";
  import { asDate, asSortLink } from "../../Shared/Actions.js";

  let apply = e => filter.set({ ..._filter, page: 1 });
  let sorted = e => ($filter.sort = e.detail);
  $: sort = $filter.sort;

  let _filter = $filter,
    data = [],
    pager = {};

  const unsubscribe = filter.subscribe(async x => {
    try {
      let q = await fetch(`/api/mols?${stringify(x)}`);
      pager = JSON.parse(q.headers.get("pager"));
      data = await q.json();
    } catch (error) {
      alert(error);
    }
  });

  let loading = true;

  onDestroy(unsubscribe);

</script>

<style>
  th {
    position: sticky;
    top: 100px;
    background-color: white;
  }
  .l {
    position: sticky;
    left: 20px;
    background-color: white;
  }
</style>

<Layout filter={_filter} on:apply={apply}>
  <div class="container-fluid">

    {#if loading}L O A D I N G{/if}

    <h5>Сотрудники.</h5>

    <table class="table table-striped table-sm">
      <thead>
        <tr>
          <th class="l">#</th>
          <th />
          <th>
            <button class="btn btn-link btn-sm" use:asSortLink={'MOL_ID'}>
              Код
            </button>
          </th>
          <th>
            <button class="btn btn-link btn-sm" use:asSortLink={'NAME'}>
              Имя
            </button>
          </th>
          <th>
            <button class="btn btn-link btn-sm" use:asSortLink={'MNAME_LOGON'}>
              Логин
            </button>
          </th>
          <th>
            <button class="btn btn-link btn-sm" use:asSortLink={'EMAIL'}>
              Email
            </button>
          </th>
          <th>
            <button class="btn btn-link btn-sm" use:asSortLink={'PHONE_LOCAL'}>
              Тел
            </button>
          </th>
          <th>
            <button class="btn btn-link btn-sm" use:asSortLink={'DEPT_NAME'}>
              Отдел
            </button>
          </th>
          <th>
            <button class="btn btn-link btn-sm" use:asSortLink={'POST_NAME'}>
              Пост
            </button>
          </th>
          <th>
            <button class="btn btn-link btn-sm" use:asSortLink={'DATE_HIRE'}>
              Дата
            </button>
          </th>
          <th />
        </tr>
      </thead>

      <tbody>
        {#each data as item, i}
          <tr>
            <td class="l">{i + pager.offset + 1}</td>
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
            <td use:asDate={item.DATE_HIRE} />
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
