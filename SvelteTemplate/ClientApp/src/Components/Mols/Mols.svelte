<script context="module">
  import { writable } from "svelte/store";
  export const filter = writable({});

</script>

<script>
  import Layout from "../../Layout.svelte";
  import Pager from "../../Shared/Pager.svelte";
  import Filter from "./Filter.svelte";
  import { stringify } from "query-string";

  import { onMount, onDestroy, getContext } from "svelte";

  import { asDate, asSortLink } from "../../Shared/Actions.js";

  import { route, reglament } from "../../store";

  let apply = e => filter.set({ ..._filter, page: 1 });
  let sorted = e => ($filter.sort = e.detail);
  $: sort = $filter.sort;

  let _filter = $filter,    data = [],    pager = {};


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
  /* th {
    position: sticky;
    top: 100px;
    background-color: white;
  } */
  .deleted td {
    text-decoration: line-through;
    opacity: 0.5;
    }
</style>

<Layout filter={_filter} on:apply={apply}>
  <div class="container">

<!-- 
    <nav class="navbar navbar-expand navbar-light bg-light shadow-sm mb-3 rounded">
      <div class="navbar-brand mr-auto">Сотрудники</div>
      <div class="nav navbar-nav">
        <div class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" type="button" id="triggerId" data-toggle="dropdown"
            aria-haspopup="true" aria-expanded="false">
            Действия
          </a>
          <div class="dropdown-menu dropdown-menu-right" aria-labelledby="triggerId">
            <a class="dropdown-item" href="/mols/new" class:disabled={!$reglament.allowEdit}>
              Новый сотрудник
            </a>
          </div>
        </div>
      </div>
    </nav> -->


    

      <table class="table table-sm table-striped table-hover shadow">
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

          </tr>
        </thead>

        <tbody>
          {#each data as item, i}
          <tr class:deleted={!item.IS_WORKING}>
            <td class="l">{i + pager.offset + 1}</td>
            <td>
              <a href="/mols/{item.MOL_ID}/common">
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
    <Filter bind:filter={_filter} />
  </div>

  <div slot="actions">
{#if $reglament}    
              <a class="dropdown-item" href="/mols/new" class:disabled={!$reglament.allowEdit}>
              Новый сотрудник
            </a>
{/if}  
    </div>

</Layout>




